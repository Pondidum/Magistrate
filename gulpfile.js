var gulp = require("gulp");
var shell = require("gulp-shell");
var args = require('yargs').argv;
var fs = require("fs");
var assemblyInfo = require('gulp-dotnet-assembly-info');
var rename = require('gulp-rename');
var msbuild = require('gulp-msbuild');

var project = JSON.parse(fs.readFileSync("./package.json"));

var config = {
  name: project.name,
  version: project.version,
  mode: args.mode || "Debug",
  commit: process.env.APPVEYOR_REPO_COMMIT || "0",
  buildNumber: process.env.APPVEYOR_BUILD_VERSION || "0",
  output: "./build/deploy"
}

gulp.task("default", [ "restore", "version", "compile" ], function() {
  console.log(config.name, config.version);
});

gulp.task("ci", []);


gulp.task('restore', function() {
  return gulp
    .src(config.name + '.sln', { read: false })
    .pipe(shell('"./tools/nuget/nuget.exe" restore '));
});

gulp.task('version', function() {
  return gulp
    .src(config.name + '/Properties/AssemblyVersion.base')
    .pipe(rename("AssemblyVersion.cs"))
    .pipe(assemblyInfo({
      version: config.version,
      fileVersion: config.version,
      description: "Build: " +  config.buildNumber + ", Sha: " + config.commit
    }))
    .pipe(gulp.dest('./' + config.name + '/Properties'));
});

gulp.task('compile', [ "restore", "version" ], function() {
  return gulp
    .src(config.name + ".sln")
    .pipe(msbuild({
      targets: [ "Clean", "Rebuild" ],
      configuration: config.mode,
      toolsVersion: 14.0,
      errorOnFail: true,
      stdout: true,
      verbosity: "minimal"
    }));
});
