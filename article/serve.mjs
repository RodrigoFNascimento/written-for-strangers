// Tiny static server for previews.
//   node serve.mjs <dir> [port=4173] [prefix=/]
// With a prefix (e.g. /my-talk/) it mimics GitHub Pages project URLs.
import { createServer } from 'node:http';
import { readFile, stat } from 'node:fs/promises';
import { join, resolve, extname, normalize } from 'node:path';

const [dir = 'dist-preview', port = '4173', prefix = '/'] = process.argv.slice(2);
const root = resolve(dir);
const TYPES = { '.html': 'text/html; charset=utf-8', '.css': 'text/css', '.js': 'text/javascript', '.mjs': 'text/javascript', '.json': 'application/json', '.svg': 'image/svg+xml', '.png': 'image/png', '.jpg': 'image/jpeg', '.ico': 'image/x-icon', '.woff2': 'font/woff2', '.wasm': 'application/wasm' };

createServer(async (req, res) => {
  let path = decodeURIComponent(new URL(req.url, 'http://x').pathname);
  if (!path.startsWith(prefix)) { res.writeHead(302, { Location: prefix }); return res.end(); }
  path = normalize(path.slice(prefix.length)).replace(/^([/\\])+/, '');
  let file = join(root, path);
  if (!file.startsWith(root)) { res.writeHead(403); return res.end(); }
  try {
    if ((await stat(file)).isDirectory()) file = join(file, 'index.html');
  } catch {
    // SPA fallback for Slidev routes like /slides/3
    const spa = path.split(/[/\\]/)[0];
    file = join(root, spa, 'index.html');
  }
  try {
    const body = await readFile(file);
    res.writeHead(200, { 'Content-Type': TYPES[extname(file)] || 'application/octet-stream' });
    res.end(body);
  } catch {
    res.writeHead(404); res.end('Not found');
  }
}).listen(Number(port), () => console.log(`serving ${root} at http://localhost:${port}${prefix}`));
