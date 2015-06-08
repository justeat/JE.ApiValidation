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
