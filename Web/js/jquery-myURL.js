﻿/* - - - - - - - - - - - - - - - - - - - - - - - - - - - */
/* - - - - - - - - PLUG-IN BEGIN - - - - - - - - - - - - */
/* - - - - - - - - - - - - - - - - - - - - - - - - - - - */
(function (a) {
    a.myURL || (a.extend({
        myURL: function (d) {
            a.myURL.defaults.base || a.myURL.config(); if (d) {
                var b, c = a.myURL.defaults.base; if ("index" == d) c += "/index.php", b = Array.prototype.slice.call(arguments, 1); else { if ("pathname" == d) return window.location.pathname; if ("afterpath" == d) return window.location.href.substr(window.location.href.indexOf(window.location.pathname) + window.location.pathname.length); if ("fullpathname" == d) return a.myURL("pathname") + a.myURL("afterpath"); b = Array.prototype.slice.call(arguments) } if (0 <
                b.length) {
                    var f = [], e = void 0; for (x in b) if ("string" === typeof b[x]) "" == !b[x] && ("/" == b[x].charAt(0) && (b[x] = b[x].substring(1)), "/" == b[x].charAt(b[x].length - 1) && (b[x] = b[x].substring(0, b[x].length - 1)), "" == !b[x] && (f[x] = b[x])); else if ("object" === typeof b[x]) {
                        var g = !1; if (0 < f.length) for (y in f) -1 < f[y].indexOf("?") && (g = !0); if ("[object Array]" === Object.prototype.toString.call(b[x])) for (y in b[x]) if ("string" == typeof b[x][y]) e += (g ? "&" : e ? -1 < e.indexOf("?") ? "&" : "?" : "?") + b[x][y]; else {
                            if ("object" == typeof b[x][y]) var h =
                            b[x][y].key + "=" + b[x][y].value, e = e ? e + ((-1 < e.indexOf("?") ? "&" : g ? "&" : "?") + h) : (g ? "&" : "?") + h
                        } else e = e ? e + ((-1 < e.indexOf("?") ? "&" : g ? "&" : "?") + a.param(b[x])) : (g ? "&" : "?") + a.param(b[x])
                    } 0 < f.length && (c = c + "/" + f.join("/")); e && (-1 < c.indexOf("/?") && (c = c.replace("/?", "?")), c += e)
                } c = a.myURL.defaults.trail ? c.charAt(c.length - 1) ? c + "/" : c : c; b = c.split("/"); if (b["" == b[b.length - 1] ? b.length - 2 : b.length - 1].match(/.[.,!,@,#,$,%,^,&,*,?,_,~]/)) "/" == c.charAt(c.length - 1) && (c = c.substring(0, c.length - 1)); return c
            } c = a.myURL.defaults.base;
            return c = a.myURL.defaults.trail ? c.charAt(c.length - 1) ? c + "/" : c : c
        }
    }), a.myURL.config = function (d) {
        var b = location.href; if (d) a.myURL.defaults.isLocal = d.isLocal ? !0 : 0 <= b.indexOf("//local") ? !0 : !1, d.forceMain && (a.myURL.defaults.forceMain = d.forceMain), d.local && (d.local.main && (a.myURL.defaults.dirs.local.main = d.local.main), d.local.sub && (a.myURL.defaults.dirs.local.sub = d.local.sub)), d.onSite && (d.onSite.main && (a.myURL.defaults.dirs.onSite.main = d.onSite.main), d.onSite.sub && (a.myURL.defaults.dirs.onSite.sub = d.onSite.sub)),
        d.trail && (a.myURL.defaults.trail = d.trail); else { d = {}; var c = b.substring(b.indexOf(window.location.hostname)).split("/"); 2 <= c.length && ("" != c[1] && !c[1].match(/.[.,!,@,#,$,%,^,&,*,?,_,~]/)) && (a.myURL.defaults.mainDir = c[1], 3 <= c.length && (c.splice(0, 2), d = [], d = function () { var a = []; for (x in c) if ("" != c[x] && !c[x].match(/.[.,!,@,#,$,%,^,&,*,?,_,~]/)) a[x] = c[x]; else break; return a }, 0 < d.length && (a.myURL.defaults.subDir = d.join("/")))) } a.myURL.defaults.isLocal ? a.myURL.defaults.dirs.local.main && (b = b.substring(b.indexOf(a.myURL.defaults.dirs.local.main)).substring(0,
        b.substring(b.indexOf(a.myURL.defaults.dirs.local.main)).indexOf("/")), a.myURL.defaults.dirs.local.main == b && (a.myURL.defaults.mainDir = a.myURL.defaults.dirs.local.main), a.myURL.defaults.dirs.local.sub && (a.myURL.defaults.subDir = a.myURL.defaults.dirs.local.sub)) : a.myURL.defaults.dirs.onSite.main && (a.myURL.defaults.forceMain ? a.myURL.defaults.dirs.onSite.main && (a.myURL.defaults.mainDir = a.myURL.defaults.dirs.onSite.main) : (b = b.substring(b.indexOf(a.myURL.defaults.dirs.onSite.main)).substring(0, b.substring(b.indexOf(a.myURL.defaults.dirs.onSite.main)).indexOf("/")),
        a.myURL.defaults.dirs.onSite.main == b && (a.myURL.defaults.mainDir = a.myURL.defaults.dirs.onSite.main)), a.myURL.defaults.dirs.onSite.sub && (a.myURL.defaults.subDir = a.myURL.defaults.dirs.onSite.sub)); a.myURL.defaults.base = window.location.protocol + "//" + window.location.hostname; a.myURL.defaults.mainDir && (a.myURL.defaults.base = a.myURL.defaults.base + "/" + a.myURL.defaults.mainDir); a.myURL.defaults.subDir && (a.myURL.defaults.base = a.myURL.defaults.base + "/" + a.myURL.defaults.subDir)
    }, a.myURL.defaults = {
        base: void 0,
        dirs: { local: { main: void 0, sub: void 0 }, onSite: { main: void 0, sub: void 0 } }, forceMain: !0, isLocal: !1, mainDir: void 0, subDir: void 0, trail: !0
    })
})(jQuery);
/* - - - - - - - - - - - - - - - - - - - - - - - - - - - */
/* - - - - - - - - PLUG-IN END - - - - - - - - - - - - - */
/* - - - - - - - - - - - - - - - - - - - - - - - - - - - */