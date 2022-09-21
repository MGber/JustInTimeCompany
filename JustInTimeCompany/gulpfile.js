/// <binding AfterBuild='clean, minimg, minjs, mincss' />
"use strict";

import gulp from "gulp";
import concat from "gulp-concat";
import cssmin from "gulp-cssmin";
import uglify from "gulp-uglify";
import clean from "gulp-clean";
import imagemin from "gulp-imagemin";

var webRootFolder = "wwwroot";
var paths = { webroot: `./${webRootFolder}/` };
paths.js = paths.webroot + "js/**/*.js";
paths.css = paths.webroot + "css/**/*.css";
paths.myLib = paths.webroot + "myLib";
paths.img = paths.webroot + "assets/*";

gulp.task("clean", function (cb) {
    console.log(paths.myLib);
    return gulp.src(paths.myLib)
        .pipe(clean({ force: true }));
});



gulp.task("minjs", function () {
    return gulp.src(paths.js)
        .pipe(concat("site.min.js"))
        .pipe(uglify())
        .pipe(gulp.dest(paths.myLib));
});

gulp.task("mincss", function () {
    return gulp.src(paths.css)
        .pipe(concat("site.min.css"))
        .pipe(cssmin())
        .pipe(gulp.dest(paths.myLib));
});

gulp.task("minimg",
    function () {
        return gulp.src(paths.img)
            .pipe(imagemin())
            .pipe(gulp.dest(paths.myLib));
    });