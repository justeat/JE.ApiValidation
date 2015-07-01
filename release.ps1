param(
  [Parameter(Mandatory=$true, HelpMessage="The version number to publish, eg 1.2.3. Set this in CI first.")]
  [string] $version,
  [Parameter(Mandatory=$false, HelpMessage="CI project name")]
  [string] $name = "je-apivalidation",
  [Parameter(Mandatory=$false, HelpMessage="CI project owner")]
  [string] $owner = "justeattech"
)

if (($version -eq $null) -or ($version -eq '')) {
  # TODO: validate that a tag like this doesn't exist already
  throw "Must supply version number in semver format eg 1.2.3"
}
$ci = "https://ci.appveyor.com/project/$owner/$name"
$tag = "v$version"
$release = "release-$version"
write-host "Your current status" -foregroundcolor green
& git status
write-host "Stashing any work and checking out master" -foregroundcolor green
& git stash
& git checkout master
write-host "Tagging & branching. tag: $tag / branch: $release" -foregroundcolor green
& git tag -a $tag -m "Release $tag"
& git checkout $tag
& git checkout -b $release
# TODO: bounty - do this in code against api
# http://www.appveyor.com/docs/api/projects-builds#update-project
write-host "We'll pause now while you remember to bump the version number in CI ($ci/settings) to match the version you're releasing ;-)"
read-host "hit enter when you've done that..."
write-host "Pushing" -foregroundcolor green
& git push --tags upstream -u $release
write-host "Done."
write-host "Check $ci"
& git checkout master
write-host "Putting you back on master branch" -foregroundcolor green
exit 0
