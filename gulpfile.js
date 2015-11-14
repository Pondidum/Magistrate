var gulp = require("gulp");
var shell = require("gulp-shell");

gulp.task("default", []);

gulp.task("ci", []);


gulp.task('restore', function() {
  return gulp
    .src('Magistrate.sln', { read: false })
    .pipe(shell('"./tools/nuget/nuget.exe" restore '));
});
