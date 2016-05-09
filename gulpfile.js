var gulp = require("gulp");
var shell = require("gulp-shell");
var args = require('yargs').argv;
var fs = require("fs");
var assemblyInfo = require('gulp-dotnet-assembly-info');
var rename = require('gulp-rename');
var msbuild = require('gulp-msbuild');
var xunit =require('gulp-xunit-runner');
var webpack = require("webpack-stream");
var debug = require('gulp-debug');


var concat = require('gulp-concat');
var react = require('gulp-react');

var project = JSON.parse(fs.readFileSync("./package.json"));

var config = {
  name: project.name,
  version: project.version,
  mode: args.mode || "Debug",
  commit: process.env.APPVEYOR_REPO_COMMIT || "0",
  buildNumber: process.env.APPVEYOR_BUILD_VERSION || "0",
  output: "./build/deploy"
}

gulp.task("default", [ "restore", "version", "compile", "compile:test", "pack" ]);

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

gulp.task('transform', function() {
  return gulp
    .src('./Magistrate/client/index.js')
    .pipe(webpack(require('./webpack.config.js')))
    .pipe(gulp.dest('./Magistrate/client/static/'));
})

gulp.task('compile', [ "transform", "restore", "version" ], function() {
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

var test = function() {
  return gulp
    .src(['**/bin/*/*.Tests.dll'], { read: false })
    .pipe(xunit({
      executable: './packages/xunit.runner.console.2.1.0/tools/xunit.console.exe',
      options: {
        verbose: true,
        nologo: true,
      }
    }));
};

gulp.task('compile:test', [ "compile" ], test);
gulp.task('test', test);

gulp.task('pack', [ 'compile:test' ], function () {
  return gulp
    .src('**/*.nuspec', { read: false })
    .pipe(rename({ extname: ".csproj" }))
    .pipe(shell([
      '"tools/nuget/nuget.exe" pack <%= file.path %> -version <%= version %> -prop configuration=<%= mode %> -o <%= output%>'
    ], {
      templateData: config
    }));
});
