---
description: 'Guidelines for building commit messages'
---

Conventional Commits 1.0.0
==========================

[](https://www.conventionalcommits.org/en/v1.0.0/#summary)Summary
-----------------------------------------------------------------

The Conventional Commits specification is a lightweight convention on top of commit messages. It provides an easy set of rules for creating an explicit commit history; which makes it easier to write automated tools on top of. This convention dovetails with [SemVer](http://semver.org/), by describing the features, fixes, and breaking changes made in commit messages.

The commit message should be structured as follows:

* * * * *

```
<type>[optional scope]: <description>

[optional body]

[optional footer(s)]

```

* * * * *

The commit contains the following structural elements, to communicate intent to the consumers of your library:

1.  fix: a commit of the *type* `fix` patches a bug in your codebase (this correlates with [`PATCH`](http://semver.org/#summary) in Semantic Versioning).
2.  feat: a commit of the *type* `feat` introduces a new feature to the codebase (this correlates with [`MINOR`](http://semver.org/#summary) in Semantic Versioning).
3.  BREAKING CHANGE: a commit that has a footer `BREAKING CHANGE:`, or appends a `!` after the type/scope, introduces a breaking API change (correlating with [`MAJOR`](http://semver.org/#summary) in Semantic Versioning). A BREAKING CHANGE can be part of commits of any *type*.
4.  *types* other than `fix:` and `feat:` are allowed, for example [@commitlint/config-conventional](https://github.com/conventional-changelog/commitlint/tree/master/%40commitlint/config-conventional) (based on the [Angular convention](https://github.com/angular/angular/blob/22b96b9/CONTRIBUTING.md#-commit-message-guidelines)) recommends `build:`, `chore:`, `ci:`, `docs:`, `style:`, `refactor:`, `perf:`, `test:`, and others.
5.  *footers* other than `BREAKING CHANGE: <description>` may be provided and follow a convention similar to [git trailer format](https://git-scm.com/docs/git-interpret-trailers).

Additional types are not mandated by the Conventional Commits specification, and have no implicit effect in Semantic Versioning (unless they include a BREAKING CHANGE). A scope may be provided to a commit's type, to provide additional contextual information and is contained within parenthesis, e.g., `feat(parser): add ability to parse arrays`.

[](https://www.conventionalcommits.org/en/v1.0.0/#examples)Examples
-------------------------------------------------------------------

### [](https://www.conventionalcommits.org/en/v1.0.0/#commit-message-with-description-and-breaking-change-footer)Commit message with description and breaking change footer

```
feat: allow provided config object to extend other configs

BREAKING CHANGE: `extends` key in config file is now used for extending other config files

```

### [](https://www.conventionalcommits.org/en/v1.0.0/#commit-message-with--to-draw-attention-to-breaking-change)Commit message with `!` to draw attention to breaking change

```
feat!: send an email to the customer when a product is shipped

```

### [](https://www.conventionalcommits.org/en/v1.0.0/#commit-message-with-scope-and--to-draw-attention-to-breaking-change)Commit message with scope and `!` to draw attention to breaking change

```
feat(api)!: send an email to the customer when a product is shipped

```

### [](https://www.conventionalcommits.org/en/v1.0.0/#commit-message-with-both--and-breaking-change-footer)Commit message with both `!` and BREAKING CHANGE footer

```
chore!: drop support for Node 6

BREAKING CHANGE: use JavaScript features not available in Node 6.

```

### [](https://www.conventionalcommits.org/en/v1.0.0/#commit-message-with-no-body)Commit message with no body

```
docs: correct spelling of CHANGELOG

```

### [](https://www.conventionalcommits.org/en/v1.0.0/#commit-message-with-scope)Commit message with scope

```
feat(lang): add Polish language

```

### [](https://www.conventionalcommits.org/en/v1.0.0/#commit-message-with-multi-paragraph-body-and-multiple-footers)Commit message with multi-paragraph body and multiple footers

```
fix: prevent racing of requests

Introduce a request id and a reference to latest request. Dismiss
incoming responses other than from latest request.

Remove timeouts which were used to mitigate the racing issue but are
obsolete now.

Reviewed-by: Z
Refs: #123

```

[](https://www.conventionalcommits.org/en/v1.0.0/#specification)Specification
-----------------------------------------------------------------------------

The key words "MUST", "MUST NOT", "REQUIRED", "SHALL", "SHALL NOT", "SHOULD", "SHOULD NOT", "RECOMMENDED", "MAY", and "OPTIONAL" in this document are to be interpreted as described in [RFC 2119](https://www.ietf.org/rfc/rfc2119.txt).

1.  Commits MUST be prefixed with a type, which consists of a noun, `feat`, `fix`, etc., followed by the OPTIONAL scope, OPTIONAL `!`, and REQUIRED terminal colon and space.
2.  The type `feat` MUST be used when a commit adds a new feature to your application or library.
3.  The type `fix` MUST be used when a commit represents a bug fix for your application.
4.  A scope MAY be provided after a type. A scope MUST consist of a noun describing a section of the codebase surrounded by parenthesis, e.g., `fix(parser):`
5.  A description MUST immediately follow the colon and space after the type/scope prefix. The description is a short summary of the code changes, e.g., *fix: array parsing issue when multiple spaces were contained in string*.
6.  A longer commit body MAY be provided after the short description, providing additional contextual information about the code changes. The body MUST begin one blank line after the description.
7.  A commit body is free-form and MAY consist of any number of newline separated paragraphs.
8.  One or more footers MAY be provided one blank line after the body. Each footer MUST consist of a word token, followed by either a `:<space>` or `<space>#` separator, followed by a string value (this is inspired by the [git trailer convention](https://git-scm.com/docs/git-interpret-trailers)).
9.  A footer's token MUST use `-` in place of whitespace characters, e.g., `Acked-by` (this helps differentiate the footer section from a multi-paragraph body). An exception is made for `BREAKING CHANGE`, which MAY also be used as a token.
10. A footer's value MAY contain spaces and newlines, and parsing MUST terminate when the next valid footer token/separator pair is observed.
11. Breaking changes MUST be indicated in the type/scope prefix of a commit, or as an entry in the footer.
12. If included as a footer, a breaking change MUST consist of the uppercase text BREAKING CHANGE, followed by a colon, space, and description, e.g., *BREAKING CHANGE: environment variables now take precedence over config files*.
13. If included in the type/scope prefix, breaking changes MUST be indicated by a `!` immediately before the `:`. If `!` is used, `BREAKING CHANGE:` MAY be omitted from the footer section, and the commit description SHALL be used to describe the breaking change.
14. Types other than `feat` and `fix` MAY be used in your commit messages, e.g., *docs: update ref docs.*
15. The units of information that make up Conventional Commits MUST NOT be treated as case sensitive by implementors, with the exception of BREAKING CHANGE which MUST be uppercase.
16. BREAKING-CHANGE MUST be synonymous with BREAKING CHANGE, when used as a token in a footer.
17. 一律使用繁體中文撰寫.
