// top-level namespace
var ProjectSafehouse = ProjectSafehouse || {};

// Code pattern from Sotyan Stefanov
function extend(ns, ns_string) {

    var parts = ns_string.split('.');
    var parent = ns;
    var pl;

    if (parts[0] == "ProjectSafehouse") {
        parts = parts.slice(1);
    }

    pl = parts.length;

    for (var i = 0; i < pl; i++) {
        if (typeof parent[parts[i]] == 'undefined') {
            parent[parts[i]] = {};
        }

        parent = parent[parts[i]];
    }

    return parent;
}