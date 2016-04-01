# Contributing

The easier your PRs are to review and merge, the more likely your contribution will be accepted. :-)

Note that the `JE.ApiValidation.DTOs` assembly is a contract that various clients will be depending on. Be very careful of breaking changes.

## Process

* Work in a feature branch in a fork, PR to our master
* One logical change per PR, please - do refactorings in separate PRs, ahead of your feature change(s)
* Have [editorconfig plugin](http://editorconfig.org) for your editor(s) installed so that your file touches are consistent with ours and the diff is reduced.
* Test coverage should not go down
* Flag breaking changes in your PR description
* Add a comment linking to passing tests in CI, proof in Kibana dashboards ("share temporary"), etc
* Link to any specifications / JIRAs that you're working against if applicable
* CI should be green!

## Releases

Releases (and version bumps & related activity) happen from a clean, up-to-date `master` branch.

* CI should be green on `master`
* Bump the version number in [appveyor.yml] - follow [SemVer rules](http://semver.org)
* Update the [CHANGELOG.md] with the `major`.`minor`.`patch` entry (omit `build` - CI will produce a new build based on the tag that the release script is about to create)
* Run [release.ps1] with the version that [appveyor.yml] now has
```powershell
./release.ps1 -version 1.2.3
```
* CI should, as a consequence of your running [release.ps1] above:
  * build the tag
  * push nuget packages to nuget.org
