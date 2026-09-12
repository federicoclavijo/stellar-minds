document.addEventListener("DOMContentLoaded", function () {

    /* ---------- Navbar: fondo al hacer scroll + link activo ---------- */
    var nav = document.querySelector(".landing-nav");
    var navLinks = document.querySelectorAll(".landing-links a[data-section]");
    var sections = document.querySelectorAll("[data-observe-section]");

    function onScroll() {
        if (!nav) return;
        if (window.scrollY > 24) {
            nav.classList.add("scrolled");
        } else {
            nav.classList.remove("scrolled");
        }
    }
    onScroll();
    window.addEventListener("scroll", onScroll, { passive: true });

    if ("IntersectionObserver" in window && sections.length) {
        var navObserver = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    var id = entry.target.getAttribute("id");
                    navLinks.forEach(function (link) {
                        link.classList.toggle("active", link.getAttribute("data-section") === id);
                    });
                }
            });
        }, { rootMargin: "-45% 0px -45% 0px" });

        sections.forEach(function (section) { navObserver.observe(section); });
    }

    /* ---------- Menú mobile ---------- */
    var toggle = document.querySelector(".nav-toggle");
    var links = document.querySelector(".landing-links");
    if (toggle && links) {
        toggle.addEventListener("click", function () {
            links.classList.toggle("mobile-open");
        });
        links.querySelectorAll("a").forEach(function (a) {
            a.addEventListener("click", function () { links.classList.remove("mobile-open"); });
        });
    }

    /* ---------- Scroll reveal genérico ---------- */
    var revealEls = document.querySelectorAll(".reveal");
    if ("IntersectionObserver" in window && revealEls.length) {
        var revealObserver = new IntersectionObserver(function (entries, obs) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    entry.target.classList.add("is-visible");
                    obs.unobserve(entry.target);
                }
            });
        }, { threshold: 0.15 });
        revealEls.forEach(function (el) { revealObserver.observe(el); });
    } else {
        revealEls.forEach(function (el) { el.classList.add("is-visible"); });
    }

    /* ---------- Contadores animados ---------- */
    var counters = document.querySelectorAll("[data-count]");
    function animateCounter(el) {
        var target = parseFloat(el.getAttribute("data-count"));
        var suffix = el.getAttribute("data-suffix") || "";
        var duration = 1400;
        var start = null;

        function step(timestamp) {
            if (!start) start = timestamp;
            var progress = Math.min((timestamp - start) / duration, 1);
            var eased = 1 - Math.pow(1 - progress, 3);
            var value = eased * target;
            el.textContent = (Number.isInteger(target) ? Math.round(value) : value.toFixed(1)) + suffix;
            if (progress < 1) {
                window.requestAnimationFrame(step);
            }
        }
        window.requestAnimationFrame(step);
    }

    if ("IntersectionObserver" in window && counters.length) {
        var counterObserver = new IntersectionObserver(function (entries, obs) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    animateCounter(entry.target);
                    obs.unobserve(entry.target);
                }
            });
        }, { threshold: 0.4 });
        counters.forEach(function (el) { counterObserver.observe(el); });
    }

    /* ---------- Tabs de roles ---------- */
    var tabButtons = document.querySelectorAll(".role-tab-btn");
    var panels = document.querySelectorAll(".role-panel");
    tabButtons.forEach(function (btn) {
        btn.addEventListener("click", function () {
            var target = btn.getAttribute("data-role");
            tabButtons.forEach(function (b) { b.classList.remove("active"); });
            panels.forEach(function (p) { p.classList.remove("active"); });
            btn.classList.add("active");
            var panel = document.querySelector('.role-panel[data-role="' + target + '"]');
            if (panel) panel.classList.add("active");
        });
    });

    /* ---------- Starfield animado en el hero ---------- */
    var canvas = document.getElementById("starfield");
    if (canvas && canvas.getContext) {
        var ctx = canvas.getContext("2d");
        var stars = [];
        var mouseX = 0, mouseY = 0;
        var w, h;

        function resize() {
            w = canvas.width = canvas.offsetWidth;
            h = canvas.height = canvas.offsetHeight;
        }

        function makeStars() {
            var count = Math.floor((w * h) / 9000);
            stars = [];
            for (var i = 0; i < count; i++) {
                stars.push({
                    x: Math.random() * w,
                    y: Math.random() * h,
                    r: Math.random() * 1.3 + 0.3,
                    baseAlpha: Math.random() * 0.6 + 0.25,
                    twinkleSpeed: Math.random() * 0.015 + 0.004,
                    twinklePhase: Math.random() * Math.PI * 2,
                    depth: Math.random() * 0.6 + 0.2
                });
            }
        }

        function draw(t) {
            ctx.clearRect(0, 0, w, h);
            for (var i = 0; i < stars.length; i++) {
                var s = stars[i];
                var alpha = s.baseAlpha + Math.sin(t * s.twinkleSpeed + s.twinklePhase) * 0.25;
                var parallaxX = (mouseX - w / 2) * 0.02 * s.depth;
                var parallaxY = (mouseY - h / 2) * 0.02 * s.depth;
                ctx.beginPath();
                ctx.arc(s.x + parallaxX, s.y + parallaxY, s.r, 0, Math.PI * 2);
                ctx.fillStyle = "rgba(238, 241, 249," + Math.max(alpha, 0) + ")";
                ctx.fill();
            }
            window.requestAnimationFrame(draw);
        }

        window.addEventListener("resize", function () {
            resize();
            makeStars();
        });

        canvas.parentElement.addEventListener("mousemove", function (e) {
            var rect = canvas.getBoundingClientRect();
            mouseX = e.clientX - rect.left;
            mouseY = e.clientY - rect.top;
        });

        resize();
        makeStars();
        window.requestAnimationFrame(draw);
    }
});
