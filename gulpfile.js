var gulp = require("gulp");
var shell = require("gulp-shell");
var args = require('yargs').argv;


var config = {
  name: "Magistrate",
  mode: args.mode || "Debug",
  commit: process.env.APPVEYOR_REPO_COMMIT || "0",
  output: "./build/deploy"
}


gulp.task("default", [ "restore" ]);

gulp.task("ci", []);


gulp.task('restore', function() {
  return gulp
    .src(config.name + '.sln', { read: false })
    .pipe(shell('"./tools/nuget/nuget.exe" restore '));
});
