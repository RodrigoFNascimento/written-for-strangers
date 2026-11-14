// Theme toggle (remembered), language preference, code highlighting, Mermaid diagrams.
(function () {
  const root = document.documentElement;
  const store = {
    get(k) { try { return localStorage.getItem(k); } catch (e) { return null; } },
    set(k, v) { try { localStorage.setItem(k, v); } catch (e) {} },
  };

  // Remember the language the reader picked, so the root URL sends them back to it.
  store.set('lang', root.lang.toLowerCase() === 'pt-br' ? 'pt-br' : 'en');

  const themeBtn = document.getElementById('theme-btn');
  const labels = { light: themeBtn?.dataset.labelLight || 'Light', dark: themeBtn?.dataset.labelDark || 'Dark' };
  const diagrams = [...document.querySelectorAll('pre.mermaid')].map(el => ({ el, src: el.textContent }));

  function preferredTheme() {
    return store.get('theme') || (matchMedia('(prefers-color-scheme: light)').matches ? 'light' : 'dark');
  }

  async function renderDiagrams(theme) {
    if (!window.mermaid || !diagrams.length) return;
    // Measure labels with the real font, or Mermaid sizes the boxes too small and clips the text.
    if (document.fonts) await document.fonts.ready;
    for (const d of diagrams) { d.el.removeAttribute('data-processed'); d.el.textContent = d.src; }
    mermaid.initialize({ startOnLoad: false, theme: theme === 'light' ? 'default' : 'dark', fontFamily: 'Inter, sans-serif' });
    await mermaid.run({ nodes: diagrams.map(d => d.el) });
  }

  function applyTheme(theme) {
    root.dataset.theme = theme;
    document.getElementById('hl-dark').disabled = theme === 'light';
    document.getElementById('hl-light').disabled = theme !== 'light';
    if (themeBtn) themeBtn.textContent = theme === 'light' ? '☾ ' + labels.dark : '☀︎ ' + labels.light;
    renderDiagrams(theme);
  }

  themeBtn?.addEventListener('click', () => {
    const next = root.dataset.theme === 'light' ? 'dark' : 'light';
    store.set('theme', next);
    applyTheme(next);
  });

  if (window.hljs) hljs.highlightAll();
  applyTheme(preferredTheme());
})();
