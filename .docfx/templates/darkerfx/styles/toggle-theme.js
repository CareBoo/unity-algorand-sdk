const sw = document.getElementById("switch-style"), b = document.body;

if (sw && b) {
    if (window.localStorage && localStorage.getItem("theme") === "theme-dark") {
        // Browser supports local storage and dark mode is configured.
        sw.checked = true;
    }
    else {
        // Browser does not support local storage, or no stored theme setting is
        // present. Determine dark mode based on browser settings. Browsers
        // which don't support media matching will default to light theme, in
        // line with what most people would expect from a web page.
        sw.checked = window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches;
    }

    b.classList.toggle("theme-dark", sw.checked)
    b.classList.toggle("theme-light", !sw.checked)

    sw.addEventListener("change", function () {
        b.classList.toggle("theme-dark", this.checked)
        b.classList.toggle("theme-light", !this.checked)

        if (window.localStorage) {
            localStorage.setItem("theme", this.checked ? "theme-dark" : "theme-light");
        }
    })
}
