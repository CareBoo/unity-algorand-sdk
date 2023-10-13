# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).


# [5.0.0](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.1.0...v5.0.0) (2023-10-13)


### Bug Fixes

* :bug: add AsSpan and AsReadOnlySpan to NativeArray for Unity 2021.3 ([0e51971](https://github.com/CareBoo/unity-algorand-sdk/commit/0e519719c475ace0aa63dc9ac76d0c690573b1e9))
* :bug: fix compile error in test script unity 2021.3 ([462e612](https://github.com/CareBoo/unity-algorand-sdk/commit/462e6121d441f590db691fa774557d385b3c375b))
* :bug: update scripts to use explicit AsSpan APIs ([849f1b3](https://github.com/CareBoo/unity-algorand-sdk/commit/849f1b3e7c3f506304c64532306f11c7201e547c))
* :bug: use new collections API and collisions with extension methods ([6737cfe](https://github.com/CareBoo/unity-algorand-sdk/commit/6737cfedd80b584159b4f0780500c0684ab7ad97))
* **accounts:** change encryption of LocalAccountStore to prevent crashes ([#222](https://github.com/CareBoo/unity-algorand-sdk/issues/222)) ([d02fc90](https://github.com/CareBoo/unity-algorand-sdk/commit/d02fc900b1b67fc6e69d73664c747c224965bc92))
* **webgl:** :bug: remove unsupported apis on webgl ([3202b93](https://github.com/CareBoo/unity-algorand-sdk/commit/3202b93c0ca2117e1d255a23f647fbd2e7ceb6cd))


### Code Refactoring

* :fire: remove .NET SDK support ([34c9d39](https://github.com/CareBoo/unity-algorand-sdk/commit/34c9d39eea41c14281fdb0fb21ae3c6eea40d377))
* :fire: remove WalletConnect support from this SDK ([76c48bb](https://github.com/CareBoo/unity-algorand-sdk/commit/76c48bb06a170aeb0caea296b8149233051556ac))


### Features

* **algod:** :sparkles: add `round` field to `Algorand.Unity.Algod.Box` ([7796b7d](https://github.com/CareBoo/unity-algorand-sdk/commit/7796b7d1a6e071c54cb47897b466de8e32780476))
* **crypto:** :sparkles: add `PwHash` store methods ([721907b](https://github.com/CareBoo/unity-algorand-sdk/commit/721907be4571a98e58f4c6aff36efa2d50a2db59))
* **crypto:** :sparkles: add `PwHash` struct and related APIs ([6642e99](https://github.com/CareBoo/unity-algorand-sdk/commit/6642e99f759866f857a65cf748fc6a0be30f26d3))
* **crypto:** :sparkles: add `PwHash` to represent password hashes for storage ([3f9bfb3](https://github.com/CareBoo/unity-algorand-sdk/commit/3f9bfb30302c50ef861472615ce4c8e7faa42346))
* **crypto:** :sparkles: add `SecretBox` related APIs for encrypt and decrypt ([a6f7ea0](https://github.com/CareBoo/unity-algorand-sdk/commit/a6f7ea0d8783e8edc73a07d17156878fbe759dfa))
* **crypto:** :sparkles: add `SodiumArray` ([5c03fa3](https://github.com/CareBoo/unity-algorand-sdk/commit/5c03fa3dbf3a755973b22ec52322e4adbbd1e908))
* **crypto:** :sparkles: add `SodiumReference` ([11762f6](https://github.com/CareBoo/unity-algorand-sdk/commit/11762f6dd6f09337346c4c008993fa8c9d9230f1))
* **crypto:** :sparkles: add `SodiumString` ([fdd3ccb](https://github.com/CareBoo/unity-algorand-sdk/commit/fdd3ccbc3b5045839c58be7072dd1c32c9e43356))
* **crypto:** :sparkles: add mlock and munlock in sodium ([0d9a4d7](https://github.com/CareBoo/unity-algorand-sdk/commit/0d9a4d72ef430a1f5d711e2e2cb291eaa7fcba05))
* **crypto:** :sparkles: add new methods to support secret key -> seed and public keys ([ac09b7d](https://github.com/CareBoo/unity-algorand-sdk/commit/ac09b7dd2b8325ca8081721c380bb9930c99bd1e))
* **crypto:** :sparkles: add Sha256 and X25519 APIs, and use Span based APIs ([22cd7fd](https://github.com/CareBoo/unity-algorand-sdk/commit/22cd7fdbddf2fd04939afc87ac4b832bdb85dfd7))
* **crypto:** add ChaCha20 Encrypt and Decrypt functions ([c4c2672](https://github.com/CareBoo/unity-algorand-sdk/commit/c4c267230e046a67efcf238c0d45bf110b30c2f9))
* **encoding:** :sparkles: add new different base conversions, including baseN ([2d4a515](https://github.com/CareBoo/unity-algorand-sdk/commit/2d4a5159f69029c5187bf7a0d4cf302423320348))
* **json:** :sparkles: add jsonrpc utilities assembly ([d4fa97e](https://github.com/CareBoo/unity-algorand-sdk/commit/d4fa97e7e83452da3c7db923849bc196aa5d2e5c))
* **libsodium:** :sparkles: add pwhash, secretbox apis ([568b039](https://github.com/CareBoo/unity-algorand-sdk/commit/568b039db2d8d54853158ad07ce546c53a32ebee))
* **walletconnect:** :sparkles: add `WalletConnectSignError` ([01f43b3](https://github.com/CareBoo/unity-algorand-sdk/commit/01f43b3c22f596b54aefee242ac7fc1106cf312c))
* **walletconnect:** :sparkles: add relay and pairing WalletConnectV2 Apis ([286c235](https://github.com/CareBoo/unity-algorand-sdk/commit/286c235c20c1bbf6a33626bc4b27c70129c710be))


### BREAKING CHANGES

* Remove compatibility with .NET Algorand SDK
* WalletConnect is no longer supported. All related samples have been removed.

# [5.0.0-pre.3](https://github.com/CareBoo/unity-algorand-sdk/compare/v5.0.0-pre.2...v5.0.0-pre.3) (2023-10-13)


### Bug Fixes

* **webgl:** :bug: remove unsupported apis on webgl ([3202b93](https://github.com/CareBoo/unity-algorand-sdk/commit/3202b93c0ca2117e1d255a23f647fbd2e7ceb6cd))

# [5.0.0-pre.2](https://github.com/CareBoo/unity-algorand-sdk/compare/v5.0.0-pre.1...v5.0.0-pre.2) (2023-10-13)


### Features

* **algod:** :sparkles: add `round` field to `Algorand.Unity.Algod.Box` ([7796b7d](https://github.com/CareBoo/unity-algorand-sdk/commit/7796b7d1a6e071c54cb47897b466de8e32780476))

# [5.0.0-pre.1](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.1.0...v5.0.0-pre.1) (2023-10-03)


### Bug Fixes

* :bug: add AsSpan and AsReadOnlySpan to NativeArray for Unity 2021.3 ([0e51971](https://github.com/CareBoo/unity-algorand-sdk/commit/0e519719c475ace0aa63dc9ac76d0c690573b1e9))
* :bug: fix compile error in test script unity 2021.3 ([462e612](https://github.com/CareBoo/unity-algorand-sdk/commit/462e6121d441f590db691fa774557d385b3c375b))
* :bug: update scripts to use explicit AsSpan APIs ([849f1b3](https://github.com/CareBoo/unity-algorand-sdk/commit/849f1b3e7c3f506304c64532306f11c7201e547c))
* :bug: use new collections API and collisions with extension methods ([6737cfe](https://github.com/CareBoo/unity-algorand-sdk/commit/6737cfedd80b584159b4f0780500c0684ab7ad97))
* **accounts:** change encryption of LocalAccountStore to prevent crashes ([#222](https://github.com/CareBoo/unity-algorand-sdk/issues/222)) ([d02fc90](https://github.com/CareBoo/unity-algorand-sdk/commit/d02fc900b1b67fc6e69d73664c747c224965bc92))


### Code Refactoring

* :fire: remove .NET SDK support ([34c9d39](https://github.com/CareBoo/unity-algorand-sdk/commit/34c9d39eea41c14281fdb0fb21ae3c6eea40d377))
* :fire: remove WalletConnect support from this SDK ([76c48bb](https://github.com/CareBoo/unity-algorand-sdk/commit/76c48bb06a170aeb0caea296b8149233051556ac))


### Features

* **crypto:** :sparkles: add `PwHash` store methods ([721907b](https://github.com/CareBoo/unity-algorand-sdk/commit/721907be4571a98e58f4c6aff36efa2d50a2db59))
* **crypto:** :sparkles: add `PwHash` struct and related APIs ([6642e99](https://github.com/CareBoo/unity-algorand-sdk/commit/6642e99f759866f857a65cf748fc6a0be30f26d3))
* **crypto:** :sparkles: add `PwHash` to represent password hashes for storage ([3f9bfb3](https://github.com/CareBoo/unity-algorand-sdk/commit/3f9bfb30302c50ef861472615ce4c8e7faa42346))
* **crypto:** :sparkles: add `SecretBox` related APIs for encrypt and decrypt ([a6f7ea0](https://github.com/CareBoo/unity-algorand-sdk/commit/a6f7ea0d8783e8edc73a07d17156878fbe759dfa))
* **crypto:** :sparkles: add `SodiumArray` ([5c03fa3](https://github.com/CareBoo/unity-algorand-sdk/commit/5c03fa3dbf3a755973b22ec52322e4adbbd1e908))
* **crypto:** :sparkles: add `SodiumReference` ([11762f6](https://github.com/CareBoo/unity-algorand-sdk/commit/11762f6dd6f09337346c4c008993fa8c9d9230f1))
* **crypto:** :sparkles: add `SodiumString` ([fdd3ccb](https://github.com/CareBoo/unity-algorand-sdk/commit/fdd3ccbc3b5045839c58be7072dd1c32c9e43356))
* **crypto:** :sparkles: add mlock and munlock in sodium ([0d9a4d7](https://github.com/CareBoo/unity-algorand-sdk/commit/0d9a4d72ef430a1f5d711e2e2cb291eaa7fcba05))
* **crypto:** :sparkles: add new methods to support secret key -> seed and public keys ([ac09b7d](https://github.com/CareBoo/unity-algorand-sdk/commit/ac09b7dd2b8325ca8081721c380bb9930c99bd1e))
* **crypto:** :sparkles: add Sha256 and X25519 APIs, and use Span based APIs ([22cd7fd](https://github.com/CareBoo/unity-algorand-sdk/commit/22cd7fdbddf2fd04939afc87ac4b832bdb85dfd7))
* **crypto:** add ChaCha20 Encrypt and Decrypt functions ([c4c2672](https://github.com/CareBoo/unity-algorand-sdk/commit/c4c267230e046a67efcf238c0d45bf110b30c2f9))
* **encoding:** :sparkles: add new different base conversions, including baseN ([2d4a515](https://github.com/CareBoo/unity-algorand-sdk/commit/2d4a5159f69029c5187bf7a0d4cf302423320348))
* **json:** :sparkles: add jsonrpc utilities assembly ([d4fa97e](https://github.com/CareBoo/unity-algorand-sdk/commit/d4fa97e7e83452da3c7db923849bc196aa5d2e5c))
* **libsodium:** :sparkles: add pwhash, secretbox apis ([568b039](https://github.com/CareBoo/unity-algorand-sdk/commit/568b039db2d8d54853158ad07ce546c53a32ebee))
* **walletconnect:** :sparkles: add `WalletConnectSignError` ([01f43b3](https://github.com/CareBoo/unity-algorand-sdk/commit/01f43b3c22f596b54aefee242ac7fc1106cf312c))
* **walletconnect:** :sparkles: add relay and pairing WalletConnectV2 Apis ([286c235](https://github.com/CareBoo/unity-algorand-sdk/commit/286c235c20c1bbf6a33626bc4b27c70129c710be))


### BREAKING CHANGES

* Remove compatibility with .NET Algorand SDK
* WalletConnect is no longer supported. All related samples have been removed.

# [5.0.0-exp.3](https://github.com/CareBoo/unity-algorand-sdk/compare/v5.0.0-exp.2...v5.0.0-exp.3) (2023-10-03)


### Bug Fixes

* **accounts:** change encryption of LocalAccountStore to prevent crashes ([#222](https://github.com/CareBoo/unity-algorand-sdk/issues/222)) ([d02fc90](https://github.com/CareBoo/unity-algorand-sdk/commit/d02fc900b1b67fc6e69d73664c747c224965bc92))

# [5.0.0-exp.2](https://github.com/CareBoo/unity-algorand-sdk/compare/v5.0.0-exp.1...v5.0.0-exp.2) (2023-09-25)


### Features

* **crypto:** :sparkles: add `PwHash` store methods ([721907b](https://github.com/CareBoo/unity-algorand-sdk/commit/721907be4571a98e58f4c6aff36efa2d50a2db59))
* **crypto:** :sparkles: add `PwHash` struct and related APIs ([6642e99](https://github.com/CareBoo/unity-algorand-sdk/commit/6642e99f759866f857a65cf748fc6a0be30f26d3))
* **crypto:** :sparkles: add `PwHash` to represent password hashes for storage ([3f9bfb3](https://github.com/CareBoo/unity-algorand-sdk/commit/3f9bfb30302c50ef861472615ce4c8e7faa42346))
* **crypto:** :sparkles: add `SecretBox` related APIs for encrypt and decrypt ([a6f7ea0](https://github.com/CareBoo/unity-algorand-sdk/commit/a6f7ea0d8783e8edc73a07d17156878fbe759dfa))
* **crypto:** :sparkles: add `SodiumArray` ([5c03fa3](https://github.com/CareBoo/unity-algorand-sdk/commit/5c03fa3dbf3a755973b22ec52322e4adbbd1e908))
* **crypto:** :sparkles: add `SodiumReference` ([11762f6](https://github.com/CareBoo/unity-algorand-sdk/commit/11762f6dd6f09337346c4c008993fa8c9d9230f1))
* **crypto:** :sparkles: add `SodiumString` ([fdd3ccb](https://github.com/CareBoo/unity-algorand-sdk/commit/fdd3ccbc3b5045839c58be7072dd1c32c9e43356))
* **crypto:** :sparkles: add mlock and munlock in sodium ([0d9a4d7](https://github.com/CareBoo/unity-algorand-sdk/commit/0d9a4d72ef430a1f5d711e2e2cb291eaa7fcba05))
* **crypto:** :sparkles: add new methods to support secret key -> seed and public keys ([ac09b7d](https://github.com/CareBoo/unity-algorand-sdk/commit/ac09b7dd2b8325ca8081721c380bb9930c99bd1e))
* **libsodium:** :sparkles: add pwhash, secretbox apis ([568b039](https://github.com/CareBoo/unity-algorand-sdk/commit/568b039db2d8d54853158ad07ce546c53a32ebee))

# [5.0.0-exp.1](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.2.0-exp.2...v5.0.0-exp.1) (2023-09-14)


### Code Refactoring

* :fire: remove .NET SDK support ([34c9d39](https://github.com/CareBoo/unity-algorand-sdk/commit/34c9d39eea41c14281fdb0fb21ae3c6eea40d377))
* :fire: remove WalletConnect support from this SDK ([76c48bb](https://github.com/CareBoo/unity-algorand-sdk/commit/76c48bb06a170aeb0caea296b8149233051556ac))


### BREAKING CHANGES

* Remove compatibility with .NET Algorand SDK
* WalletConnect is no longer supported. All related samples have been removed.

# [4.2.0-exp.2](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.2.0-exp.1...v4.2.0-exp.2) (2023-09-06)


### Bug Fixes

* :bug: add AsSpan and AsReadOnlySpan to NativeArray for Unity 2021.3 ([0e51971](https://github.com/CareBoo/unity-algorand-sdk/commit/0e519719c475ace0aa63dc9ac76d0c690573b1e9))
* :bug: fix compile error in test script unity 2021.3 ([462e612](https://github.com/CareBoo/unity-algorand-sdk/commit/462e6121d441f590db691fa774557d385b3c375b))
* :bug: update scripts to use explicit AsSpan APIs ([849f1b3](https://github.com/CareBoo/unity-algorand-sdk/commit/849f1b3e7c3f506304c64532306f11c7201e547c))
* :bug: use new collections API and collisions with extension methods ([6737cfe](https://github.com/CareBoo/unity-algorand-sdk/commit/6737cfedd80b584159b4f0780500c0684ab7ad97))


### Features

* **crypto:** :sparkles: add Sha256 and X25519 APIs, and use Span based APIs ([22cd7fd](https://github.com/CareBoo/unity-algorand-sdk/commit/22cd7fdbddf2fd04939afc87ac4b832bdb85dfd7))
* **encoding:** :sparkles: add new different base conversions, including baseN ([2d4a515](https://github.com/CareBoo/unity-algorand-sdk/commit/2d4a5159f69029c5187bf7a0d4cf302423320348))
* **json:** :sparkles: add jsonrpc utilities assembly ([d4fa97e](https://github.com/CareBoo/unity-algorand-sdk/commit/d4fa97e7e83452da3c7db923849bc196aa5d2e5c))
* **walletconnect:** :sparkles: add `WalletConnectSignError` ([01f43b3](https://github.com/CareBoo/unity-algorand-sdk/commit/01f43b3c22f596b54aefee242ac7fc1106cf312c))
* **walletconnect:** :sparkles: add relay and pairing WalletConnectV2 Apis ([286c235](https://github.com/CareBoo/unity-algorand-sdk/commit/286c235c20c1bbf6a33626bc4b27c70129c710be))

# [4.2.0-exp.1](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.1.0...v4.2.0-exp.1) (2023-08-04)


### Features

* **crypto:** add ChaCha20 Encrypt and Decrypt functions ([c4c2672](https://github.com/CareBoo/unity-algorand-sdk/commit/c4c267230e046a67efcf238c0d45bf110b30c2f9))

# [4.1.0](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0...v4.1.0) (2023-05-03)


### Bug Fixes

* **account:** set methods returning `Algorand.Unity.Crypto` types to `internal` ([835f14d](https://github.com/CareBoo/unity-algorand-sdk/commit/835f14d404fa2add655abe3ad7705c43abe762a2))
* **walletconnect:** use all addresses in a `WalletConnectAccount` as signers ([5dc7214](https://github.com/CareBoo/unity-algorand-sdk/commit/5dc7214a762e8d4eb2b3069ebb621bd723672376))


### Features

* **accounts:** add mnemonic try parse methods ([c5a6c75](https://github.com/CareBoo/unity-algorand-sdk/commit/c5a6c7557c1df7db8c26d472dcdaba1347e94032))
* **accounts:** add PrivateKey try parse methods ([4c1289f](https://github.com/CareBoo/unity-algorand-sdk/commit/4c1289fd985d86750300e3cf88439e7d85bb5574))
* **accounts:** overload Account constructor with mnemonic and string types ([2e368ee](https://github.com/CareBoo/unity-algorand-sdk/commit/2e368ee5eb2edd00b568ef25d4e499f68d8b78a9)), closes [#208](https://github.com/CareBoo/unity-algorand-sdk/issues/208)
* **applications:** add Box support to `AppCallTxn` ([e3ebfe5](https://github.com/CareBoo/unity-algorand-sdk/commit/e3ebfe59f18b5ff6a92ad7e34585f5d7176f32be))
* **blockchain:** add `MicroAlgos.FromAlgos` conversion function ([055d84a](https://github.com/CareBoo/unity-algorand-sdk/commit/055d84a0121c664af51ccd033b7dd98e2f6baf57))
* **lowlevel:** add slice struct and function to ByteArray types ([b10abad](https://github.com/CareBoo/unity-algorand-sdk/commit/b10abad153ef868cd85fac93f5a6ec6456fd8662))

# [4.1.0-pre.4](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.1.0-pre.3...v4.1.0-pre.4) (2023-04-28)


### Features

* **accounts:** add mnemonic try parse methods ([c5a6c75](https://github.com/CareBoo/unity-algorand-sdk/commit/c5a6c7557c1df7db8c26d472dcdaba1347e94032))
* **accounts:** add PrivateKey try parse methods ([4c1289f](https://github.com/CareBoo/unity-algorand-sdk/commit/4c1289fd985d86750300e3cf88439e7d85bb5574))
* **accounts:** overload Account constructor with mnemonic and string types ([2e368ee](https://github.com/CareBoo/unity-algorand-sdk/commit/2e368ee5eb2edd00b568ef25d4e499f68d8b78a9)), closes [#208](https://github.com/CareBoo/unity-algorand-sdk/issues/208)
* **lowlevel:** add slice struct and function to ByteArray types ([b10abad](https://github.com/CareBoo/unity-algorand-sdk/commit/b10abad153ef868cd85fac93f5a6ec6456fd8662))

# [4.1.0-pre.3](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.1.0-pre.2...v4.1.0-pre.3) (2023-04-25)


### Features

* **applications:** add Box support to `AppCallTxn` ([e3ebfe5](https://github.com/CareBoo/unity-algorand-sdk/commit/e3ebfe59f18b5ff6a92ad7e34585f5d7176f32be))

# [4.1.0-pre.2](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.1.0-pre.1...v4.1.0-pre.2) (2023-03-20)


### Bug Fixes

* **account:** set methods returning `Algorand.Unity.Crypto` types to `internal` ([835f14d](https://github.com/CareBoo/unity-algorand-sdk/commit/835f14d404fa2add655abe3ad7705c43abe762a2))

# [4.1.0-pre.1](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0...v4.1.0-pre.1) (2023-03-09)


### Bug Fixes

* **walletconnect:** use all addresses in a `WalletConnectAccount` as signers ([5dc7214](https://github.com/CareBoo/unity-algorand-sdk/commit/5dc7214a762e8d4eb2b3069ebb621bd723672376))


### Features

* **blockchain:** add `MicroAlgos.FromAlgos` conversion function ([055d84a](https://github.com/CareBoo/unity-algorand-sdk/commit/055d84a0121c664af51ccd033b7dd98e2f6baf57))

# [4.0.0](https://github.com/CareBoo/unity-algorand-sdk/compare/v3.0.0...v4.0.0) (2023-02-27)


### Bug Fixes

* **2020.3:** change unsupported switch syntax to if statements ([7e57e7a](https://github.com/CareBoo/unity-algorand-sdk/commit/7e57e7a1547610809a1ad1b23206188070769bb5))
* **account:** fix `TryParse` throwing `NullReferenceException` when given string is null ([142bc3b](https://github.com/CareBoo/unity-algorand-sdk/commit/142bc3bc3229de3ea9fa97f22f91ea3e68f43607))
* **algod:** fix `WaitForConfirmation` not respecting `cancellationToken` ([d0b76cd](https://github.com/CareBoo/unity-algorand-sdk/commit/d0b76cd74318f7c03bdd2f03c4e9338d5b4eddf6))
* **asset-store:** change `LinkButton` -> `Button` ([9d7fac2](https://github.com/CareBoo/unity-algorand-sdk/commit/9d7fac22987b0a6e74f73817d7f9fa0ace404e42))
* **asset-store:** move bulk of asset store package to the `Packages` folder ([a78fcc8](https://github.com/CareBoo/unity-algorand-sdk/commit/a78fcc8dbbdcdbfe2d4b21aaaf0a746a7e7f05a9)), closes [#178](https://github.com/CareBoo/unity-algorand-sdk/issues/178)
* **ci:** republish for http limit ([2d483a8](https://github.com/CareBoo/unity-algorand-sdk/commit/2d483a8c441506c6dcfce2b54eb2ad3189ee5f64))
* **crypto:** add universal sodium binary ([d3dc173](https://github.com/CareBoo/unity-algorand-sdk/commit/d3dc17329d8aa2e4504f74f6f688614ed72ffdc3)), closes [#174](https://github.com/CareBoo/unity-algorand-sdk/issues/174)
* **crypto:** use proper integer types in sodium interop ([4b4f6e6](https://github.com/CareBoo/unity-algorand-sdk/commit/4b4f6e63bc8e540f1ad9fb4f2538ad3c61420398)), closes [#187](https://github.com/CareBoo/unity-algorand-sdk/issues/187)
* **dependencies:** add dependencies to ugui and ui ([0849b7c](https://github.com/CareBoo/unity-algorand-sdk/commit/0849b7caed1ccf12dd95a56fcb4d0fa352b08467))
* **dotnet sdk:** fix incorrect input to `string.EndsWith` causing compile error in Unity 2020.3 ([c5101ea](https://github.com/CareBoo/unity-algorand-sdk/commit/c5101eab7a304993f8c320634a09146769ee2a0d))
* **dotnet:** fix test compile err ([bc33374](https://github.com/CareBoo/unity-algorand-sdk/commit/bc3337404bb0d5e624a274f59abe4f03a8124ea3))
* **dotnet:** use `Algorand2_Unity` instead of `Algorand2` ([3df8515](https://github.com/CareBoo/unity-algorand-sdk/commit/3df8515e0e137ab3b1b1599d1f4f32a33416b1e9))
* **unity-collections:** support unity collections version > 2.x.x ([025a153](https://github.com/CareBoo/unity-algorand-sdk/commit/025a1533c497d2618324efde44eb7fe651de0ef1)), closes [#192](https://github.com/CareBoo/unity-algorand-sdk/issues/192)
* **walletconnect:** fix invalid chain id in wallet connect ([abdedaf](https://github.com/CareBoo/unity-algorand-sdk/commit/abdedaf1f1d93fab1b522089c7d67b739c041443))
* **walletconnect:** handle server error messages in payloads ([4b4b4e1](https://github.com/CareBoo/unity-algorand-sdk/commit/4b4b4e16a80c2d188eccdc6441ab4f7dc4e84ea8)), closes [#173](https://github.com/CareBoo/unity-algorand-sdk/issues/173)
* **webgl:** use `NativeWebSocketClient` in editor ([5188e5f](https://github.com/CareBoo/unity-algorand-sdk/commit/5188e5fd633d9eaed29d801715ef634667331eb9)), closes [#170](https://github.com/CareBoo/unity-algorand-sdk/issues/170)


### Code Refactoring

* **account:** remove obsolete signing methods ([304ba87](https://github.com/CareBoo/unity-algorand-sdk/commit/304ba8790585af10781f37a0cd3bf1e5ad037e11))
* **dotnet:** move all dotnet conversions to `Algorand.Unity.Net` ([b0f7124](https://github.com/CareBoo/unity-algorand-sdk/commit/b0f7124bb89508ade1fe175b093479d07c77675c))
* rename `AlgoSdk` to `Algorand.Unity` ([#167](https://github.com/CareBoo/unity-algorand-sdk/issues/167)) ([d135635](https://github.com/CareBoo/unity-algorand-sdk/commit/d1356352b1f099d328797fb0f202b7507cfa9a79))
* **sodium:** split libsodium binaries into arm64 and x86_64 ([e3a0510](https://github.com/CareBoo/unity-algorand-sdk/commit/e3a051097d21fc596cfd38f0d98f125726b70baa)), closes [#155](https://github.com/CareBoo/unity-algorand-sdk/issues/155)
* **support:** remove support for 2020.3 and add support for 2022.2 ([f000cd4](https://github.com/CareBoo/unity-algorand-sdk/commit/f000cd4f45c7b0199e3258dd1863a31863e0dfd3))


### Features

* **account:** add `Address.TryParse` method ([e83ad26](https://github.com/CareBoo/unity-algorand-sdk/commit/e83ad26bc879281a78c3f5a63acba2799f7b4063))
* **accounts:** add combined interfaces for `Account` and `Signer` ([6cfee78](https://github.com/CareBoo/unity-algorand-sdk/commit/6cfee7819b43ab94d38bc916a468088681a4b38e))
* **algod:** add `LedgerStateDelta` APIs and Models ([321b013](https://github.com/CareBoo/unity-algorand-sdk/commit/321b0132a046e03866101e5b16252c397eb05d17))
* **blockchain:** add `StateProofTracking` to the `BlockHeader` ([5d63c9a](https://github.com/CareBoo/unity-algorand-sdk/commit/5d63c9a3496efde132efd4122bc1745072c34717))
* **blockchain:** add missing `TxnCommitments` to `BlockHeader` ([be20acb](https://github.com/CareBoo/unity-algorand-sdk/commit/be20acb321822b5e070d39f1a558ec405bc4e57f))
* **collections:** upgrade to com.unity.collections 1.4.0 ([59663c8](https://github.com/CareBoo/unity-algorand-sdk/commit/59663c89886aad73c2b53f0c727d7b16b080c656))
* **dotnet sdk:** add `To*API` methods to `AlgodClient` and `IndexerClient` ([7b292b6](https://github.com/CareBoo/unity-algorand-sdk/commit/7b292b66925adc41f2c6bd8016e46d4d549cf8fa))
* **dotnet:** add ability to convert between dotnet and unity types ([8810be2](https://github.com/CareBoo/unity-algorand-sdk/commit/8810be28db61886f7f9650c4611d209033ed0004))
* **dotnet:** add conversion from `Algorand.Crypto.KeyPair` to `Algorand.Unity.PrivateKey` ([cfa72ff](https://github.com/CareBoo/unity-algorand-sdk/commit/cfa72ff97a393513df63b6da09b0cd7c3f87513e))
* **dotnet:** add conversion from `Algorand.Unity.Account` to `Algorand.Algod.Model.Account` ([9691d0e](https://github.com/CareBoo/unity-algorand-sdk/commit/9691d0ebef048e939d3fdf810f5517d3d2465edb))
* **dotnet:** add conversion from `Algorand.Unity.PrivateKey` to `Algorand.Crypto` types ([#168](https://github.com/CareBoo/unity-algorand-sdk/issues/168)) ([8e8e0f5](https://github.com/CareBoo/unity-algorand-sdk/commit/8e8e0f593c5c05cf0cf70073d2cb12a382a9ac58))
* **dotnet:** update `Algorand.dll` to 1.0.0.14 ([13cf117](https://github.com/CareBoo/unity-algorand-sdk/commit/13cf1177f8e008ace56ec9ed5bcd9646626140f5))
* **dotnet:** update `Algorand.dll` to version 1.0.0.15 ([3ce4d6c](https://github.com/CareBoo/unity-algorand-sdk/commit/3ce4d6ce6205c3d1f1eea37d145ae156dc8db6ce))
* **logic:** add `Logic.TealSign` methods for ed25519verify opcodes ([f9de317](https://github.com/CareBoo/unity-algorand-sdk/commit/f9de31768e92ddd6531b0c5e8e0ed1f1bf39e093))
* **node services:** add explicit operators to convert from `IAlgoApiClient` to dotnet SDK APIs ([6a657fc](https://github.com/CareBoo/unity-algorand-sdk/commit/6a657fc9f304e59436ec49140184d0b2572511c8)), closes [#157](https://github.com/CareBoo/unity-algorand-sdk/issues/157) [#158](https://github.com/CareBoo/unity-algorand-sdk/issues/158)
* **services:** add StateProof APIs ([f0eaaf0](https://github.com/CareBoo/unity-algorand-sdk/commit/f0eaaf0c20c0b8f1205eb7591d51cf07e502462e)), closes [#160](https://github.com/CareBoo/unity-algorand-sdk/issues/160) [#162](https://github.com/CareBoo/unity-algorand-sdk/issues/162)
* **transactions:** add conversion from `AlgoSdk.Sig` to `Algorand.Signature` ([e414732](https://github.com/CareBoo/unity-algorand-sdk/commit/e414732f82ef351c344e00f2c52ef610235bf8f2))
* **transactions:** add conversions between dotnet SDK and Unity SDK signatures ([be039b1](https://github.com/CareBoo/unity-algorand-sdk/commit/be039b1f98d0e65b0ae171023f3e5846e8d4aaf2))


### Performance Improvements

* **websocket:** change `WebSocketEvent` to be a struct ([1ae34c2](https://github.com/CareBoo/unity-algorand-sdk/commit/1ae34c232ab27f38d0d2bf151a32850bfde297b2))


### BREAKING CHANGES

* **support:** Unity 2020.3 is no longer supported
* **dotnet:** Removed all explicit/implicit operators to convert Algorand.Unity types to Algorand
types. All conversions now can be done with `ToUnity` and `ToDotnet` extension methods by
referencing the `Algorand.Unity.Net` assembly.
* **sodium:** Unity 2020.3 is no longer supported
* All `AlgoSdk` namespaces are now renamed to `Algorand.Unity`. A simple find and
replace should fix it.
* **account:** Obsolete methods in `Account` class relating to signing have been removed.
* **blockchain:** Removed `RootTransaction`
* **services:** Algod no longer has `GetProof` method

# [4.0.0-pre.24](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.23...v4.0.0-pre.24) (2023-02-27)


### Bug Fixes

* **account:** fix `TryParse` throwing `NullReferenceException` when given string is null ([142bc3b](https://github.com/CareBoo/unity-algorand-sdk/commit/142bc3bc3229de3ea9fa97f22f91ea3e68f43607))
* **walletconnect:** fix invalid chain id in wallet connect ([abdedaf](https://github.com/CareBoo/unity-algorand-sdk/commit/abdedaf1f1d93fab1b522089c7d67b739c041443))

# [4.0.0-pre.23](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.22...v4.0.0-pre.23) (2023-02-22)


### Bug Fixes

* **2020.3:** change unsupported switch syntax to if statements ([7e57e7a](https://github.com/CareBoo/unity-algorand-sdk/commit/7e57e7a1547610809a1ad1b23206188070769bb5))

# [4.0.0-pre.22](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.21...v4.0.0-pre.22) (2023-02-22)


### Code Refactoring

* **support:** remove support for 2020.3 and add support for 2022.2 ([f000cd4](https://github.com/CareBoo/unity-algorand-sdk/commit/f000cd4f45c7b0199e3258dd1863a31863e0dfd3))


### BREAKING CHANGES

* **support:** Unity 2020.3 is no longer supported

# [4.0.0-pre.21](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.20...v4.0.0-pre.21) (2023-02-22)


### Bug Fixes

* **unity-collections:** support unity collections version > 2.x.x ([025a153](https://github.com/CareBoo/unity-algorand-sdk/commit/025a1533c497d2618324efde44eb7fe651de0ef1)), closes [#192](https://github.com/CareBoo/unity-algorand-sdk/issues/192)


### Features

* **account:** add `Address.TryParse` method ([e83ad26](https://github.com/CareBoo/unity-algorand-sdk/commit/e83ad26bc879281a78c3f5a63acba2799f7b4063))

# [4.0.0-pre.20](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.19...v4.0.0-pre.20) (2023-02-20)


### Bug Fixes

* **dependencies:** add dependencies to ugui and ui ([0849b7c](https://github.com/CareBoo/unity-algorand-sdk/commit/0849b7caed1ccf12dd95a56fcb4d0fa352b08467))

# [4.0.0-pre.19](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.18...v4.0.0-pre.19) (2023-02-20)


### Bug Fixes

* **asset-store:** change `LinkButton` -> `Button` ([9d7fac2](https://github.com/CareBoo/unity-algorand-sdk/commit/9d7fac22987b0a6e74f73817d7f9fa0ace404e42))

# [4.0.0-pre.18](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.17...v4.0.0-pre.18) (2023-02-14)


### Bug Fixes

* **ci:** republish for http limit ([2d483a8](https://github.com/CareBoo/unity-algorand-sdk/commit/2d483a8c441506c6dcfce2b54eb2ad3189ee5f64))

# [4.0.0-pre.17](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.16...v4.0.0-pre.17) (2023-02-14)


### Bug Fixes

* **crypto:** use proper integer types in sodium interop ([4b4f6e6](https://github.com/CareBoo/unity-algorand-sdk/commit/4b4f6e63bc8e540f1ad9fb4f2538ad3c61420398)), closes [#187](https://github.com/CareBoo/unity-algorand-sdk/issues/187)

# [4.0.0-pre.16](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.15...v4.0.0-pre.16) (2023-01-21)


### Bug Fixes

* **asset-store:** move bulk of asset store package to the `Packages` folder ([a78fcc8](https://github.com/CareBoo/unity-algorand-sdk/commit/a78fcc8dbbdcdbfe2d4b21aaaf0a746a7e7f05a9)), closes [#178](https://github.com/CareBoo/unity-algorand-sdk/issues/178)

# [4.0.0-pre.15](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.14...v4.0.0-pre.15) (2022-12-21)


### Features

* **accounts:** add combined interfaces for `Account` and `Signer` ([6cfee78](https://github.com/CareBoo/unity-algorand-sdk/commit/6cfee7819b43ab94d38bc916a468088681a4b38e))

# [4.0.0-pre.14](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.13...v4.0.0-pre.14) (2022-12-21)


### Features

* **algod:** add `LedgerStateDelta` APIs and Models ([321b013](https://github.com/CareBoo/unity-algorand-sdk/commit/321b0132a046e03866101e5b16252c397eb05d17))
* **dotnet:** update `Algorand.dll` to version 1.0.0.15 ([3ce4d6c](https://github.com/CareBoo/unity-algorand-sdk/commit/3ce4d6ce6205c3d1f1eea37d145ae156dc8db6ce))

# [4.0.0-pre.13](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.12...v4.0.0-pre.13) (2022-12-20)


### Bug Fixes

* **crypto:** add universal sodium binary ([d3dc173](https://github.com/CareBoo/unity-algorand-sdk/commit/d3dc17329d8aa2e4504f74f6f688614ed72ffdc3)), closes [#174](https://github.com/CareBoo/unity-algorand-sdk/issues/174)
* **walletconnect:** handle server error messages in payloads ([4b4b4e1](https://github.com/CareBoo/unity-algorand-sdk/commit/4b4b4e16a80c2d188eccdc6441ab4f7dc4e84ea8)), closes [#173](https://github.com/CareBoo/unity-algorand-sdk/issues/173)


### Performance Improvements

* **websocket:** change `WebSocketEvent` to be a struct ([1ae34c2](https://github.com/CareBoo/unity-algorand-sdk/commit/1ae34c232ab27f38d0d2bf151a32850bfde297b2))

# [4.0.0-pre.12](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.11...v4.0.0-pre.12) (2022-12-11)


### Bug Fixes

* **dotnet:** use `Algorand2_Unity` instead of `Algorand2` ([3df8515](https://github.com/CareBoo/unity-algorand-sdk/commit/3df8515e0e137ab3b1b1599d1f4f32a33416b1e9))

# [4.0.0-pre.11](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.10...v4.0.0-pre.11) (2022-12-10)


### Code Refactoring

* **dotnet:** move all dotnet conversions to `Algorand.Unity.Net` ([b0f7124](https://github.com/CareBoo/unity-algorand-sdk/commit/b0f7124bb89508ade1fe175b093479d07c77675c))


### Features

* **dotnet:** update `Algorand.dll` to 1.0.0.14 ([13cf117](https://github.com/CareBoo/unity-algorand-sdk/commit/13cf1177f8e008ace56ec9ed5bcd9646626140f5))


### BREAKING CHANGES

* **dotnet:** Removed all explicit/implicit operators to convert Algorand.Unity types to Algorand
types. All conversions now can be done with `ToUnity` and `ToDotnet` extension methods by
referencing the `Algorand.Unity.Net` assembly.

# [4.0.0-pre.10](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.9...v4.0.0-pre.10) (2022-12-10)


### Bug Fixes

* **webgl:** use `NativeWebSocketClient` in editor ([5188e5f](https://github.com/CareBoo/unity-algorand-sdk/commit/5188e5fd633d9eaed29d801715ef634667331eb9)), closes [#170](https://github.com/CareBoo/unity-algorand-sdk/issues/170)

# [4.0.0-pre.9](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.8...v4.0.0-pre.9) (2022-11-26)


### Bug Fixes

* **dotnet:** fix test compile err ([bc33374](https://github.com/CareBoo/unity-algorand-sdk/commit/bc3337404bb0d5e624a274f59abe4f03a8124ea3))

# [4.0.0-pre.8](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.7...v4.0.0-pre.8) (2022-11-26)


### Bug Fixes

* **algod:** fix `WaitForConfirmation` not respecting `cancellationToken` ([d0b76cd](https://github.com/CareBoo/unity-algorand-sdk/commit/d0b76cd74318f7c03bdd2f03c4e9338d5b4eddf6))

# [4.0.0-pre.7](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.6...v4.0.0-pre.7) (2022-11-26)


### Code Refactoring

* **sodium:** split libsodium binaries into arm64 and x86_64 ([e3a0510](https://github.com/CareBoo/unity-algorand-sdk/commit/e3a051097d21fc596cfd38f0d98f125726b70baa)), closes [#155](https://github.com/CareBoo/unity-algorand-sdk/issues/155)


### BREAKING CHANGES

* **sodium:** Unity 2020.3 is no longer supported

# [4.0.0-pre.6](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.5...v4.0.0-pre.6) (2022-11-25)


### Features

* **dotnet:** add conversion from `Algorand.Crypto.KeyPair` to `Algorand.Unity.PrivateKey` ([cfa72ff](https://github.com/CareBoo/unity-algorand-sdk/commit/cfa72ff97a393513df63b6da09b0cd7c3f87513e))

# [4.0.0-pre.5](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.4...v4.0.0-pre.5) (2022-11-25)


### Features

* **dotnet:** add conversion from `Algorand.Unity.Account` to `Algorand.Algod.Model.Account` ([9691d0e](https://github.com/CareBoo/unity-algorand-sdk/commit/9691d0ebef048e939d3fdf810f5517d3d2465edb))

# [4.0.0-pre.4](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.3...v4.0.0-pre.4) (2022-11-25)


### Features

* **dotnet:** add conversion from `Algorand.Unity.PrivateKey` to `Algorand.Crypto` types ([#168](https://github.com/CareBoo/unity-algorand-sdk/issues/168)) ([8e8e0f5](https://github.com/CareBoo/unity-algorand-sdk/commit/8e8e0f593c5c05cf0cf70073d2cb12a382a9ac58))

# [4.0.0-pre.3](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.2...v4.0.0-pre.3) (2022-11-23)


### Code Refactoring

* rename `AlgoSdk` to `Algorand.Unity` ([#167](https://github.com/CareBoo/unity-algorand-sdk/issues/167)) ([d135635](https://github.com/CareBoo/unity-algorand-sdk/commit/d1356352b1f099d328797fb0f202b7507cfa9a79))


### BREAKING CHANGES

* All `AlgoSdk` namespaces are now renamed to `Algorand.Unity`. A simple find and
replace should fix it.

# Changelog

All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

# [4.0.0-pre.2](https://github.com/CareBoo/unity-algorand-sdk/compare/v4.0.0-pre.1...v4.0.0-pre.2) (2022-11-20)

### Code Refactoring

- **account:** remove obsolete signing methods ([304ba87](https://github.com/CareBoo/unity-algorand-sdk/commit/304ba8790585af10781f37a0cd3bf1e5ad037e11))

### BREAKING CHANGES

- **account:** Obsolete methods in `Account` class relating to signing have been removed.

# [4.0.0-pre.1](https://github.com/CareBoo/unity-algorand-sdk/compare/v3.0.0...v4.0.0-pre.1) (2022-11-20)

### Bug Fixes

- **dotnet sdk:** fix incorrect input to `string.EndsWith` causing compile error in Unity 2020.3 ([c5101ea](https://github.com/CareBoo/unity-algorand-sdk/commit/c5101eab7a304993f8c320634a09146769ee2a0d))

### Features

- **blockchain:** add `StateProofTracking` to the `BlockHeader` ([5d63c9a](https://github.com/CareBoo/unity-algorand-sdk/commit/5d63c9a3496efde132efd4122bc1745072c34717))
- **blockchain:** add missing `TxnCommitments` to `BlockHeader` ([be20acb](https://github.com/CareBoo/unity-algorand-sdk/commit/be20acb321822b5e070d39f1a558ec405bc4e57f))
- **collections:** upgrade to com.unity.collections 1.4.0 ([59663c8](https://github.com/CareBoo/unity-algorand-sdk/commit/59663c89886aad73c2b53f0c727d7b16b080c656))
- **dotnet sdk:** add `To*API` methods to `AlgodClient` and `IndexerClient` ([7b292b6](https://github.com/CareBoo/unity-algorand-sdk/commit/7b292b66925adc41f2c6bd8016e46d4d549cf8fa))
- **dotnet:** add ability to convert between dotnet and unity types ([8810be2](https://github.com/CareBoo/unity-algorand-sdk/commit/8810be28db61886f7f9650c4611d209033ed0004))
- **logic:** add `Logic.TealSign` methods for ed25519verify opcodes ([f9de317](https://github.com/CareBoo/unity-algorand-sdk/commit/f9de31768e92ddd6531b0c5e8e0ed1f1bf39e093))
- **node services:** add explicit operators to convert from `IAlgoApiClient` to dotnet SDK APIs ([6a657fc](https://github.com/CareBoo/unity-algorand-sdk/commit/6a657fc9f304e59436ec49140184d0b2572511c8)), closes [#157](https://github.com/CareBoo/unity-algorand-sdk/issues/157) [#158](https://github.com/CareBoo/unity-algorand-sdk/issues/158)
- **services:** add StateProof APIs ([f0eaaf0](https://github.com/CareBoo/unity-algorand-sdk/commit/f0eaaf0c20c0b8f1205eb7591d51cf07e502462e)), closes [#160](https://github.com/CareBoo/unity-algorand-sdk/issues/160) [#162](https://github.com/CareBoo/unity-algorand-sdk/issues/162)
- **transactions:** add conversion from `Algorand.Unity.Sig` to `Algorand.Signature` ([e414732](https://github.com/CareBoo/unity-algorand-sdk/commit/e414732f82ef351c344e00f2c52ef610235bf8f2))
- **transactions:** add conversions between dotnet SDK and Unity SDK signatures ([be039b1](https://github.com/CareBoo/unity-algorand-sdk/commit/be039b1f98d0e65b0ae171023f3e5846e8d4aaf2))

### BREAKING CHANGES

- **blockchain:** Removed `RootTransaction`
- **services:** Algod no longer has `GetProof` method

# [3.0.0](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.3.0...v3.0.0) (2022-07-16)

### Bug Fixes

- **abi:** fix Tuple not encoding dynamic types correctly ([be125d3](https://github.com/CareBoo/unity-algorand-sdk/commit/be125d3b7908a95d8a373f30aa5b081cb358267f))
- **algod:** fix `AlgodClient.WaitForConfirmation` not using correct wait time ([169be0b](https://github.com/CareBoo/unity-algorand-sdk/commit/169be0b4745fdf954a2c6cacc8c6b6deabd51b36))
- **collections:** fix obsolete API in most recent Unity Collections package ([cf05ec9](https://github.com/CareBoo/unity-algorand-sdk/commit/cf05ec9bff584aa7dab37e835427e004a52d5d2d))
- **inspector:** fix gaps between label and property in custom property drawers ([2ad65da](https://github.com/CareBoo/unity-algorand-sdk/commit/2ad65da3a44b3ac422be11be8483b8c1b931290c))
- **json:** fix `JsonReader.ReadNumber` for integers when given fraction with exponent ([8bf0758](https://github.com/CareBoo/unity-algorand-sdk/commit/8bf0758209d451f5310e9d64fda169758ad0f521))
- **json:** fix strings failing to be read on `null` ([dac58b7](https://github.com/CareBoo/unity-algorand-sdk/commit/dac58b7ee873a5443080429faad3efcfd6cff727))
- **kmd:** fix `KmdAccount` not renewing wallet handle tokens correctly ([1b0479d](https://github.com/CareBoo/unity-algorand-sdk/commit/1b0479dd2b54b0c0c4820018fd2d50f2440021ea))
- **lowlevel:** fix `NativeListOfList` not correctly assigning indices with `IIndexable<>` types ([aaa3b15](https://github.com/CareBoo/unity-algorand-sdk/commit/aaa3b15ddf906eb1cde4530a0261562584dd136b))
- **msgpack:** fix MessagePackWriter.Bytes issue when writing empty byte array ([eef81de](https://github.com/CareBoo/unity-algorand-sdk/commit/eef81de2b80c74a6b9eac132ec33e6c7528daaf4))
- **walletconnect:** `WalletConnectAccount.SignTxns` can now be done without calling `BeginSession` ([84bcba9](https://github.com/CareBoo/unity-algorand-sdk/commit/84bcba94c4ceab8753cfdce7addfa80bd4ac91d1))
- **walletconnect:** add checks to `AlgorandWalletConnectSession(SessionData)` ([7d0397d](https://github.com/CareBoo/unity-algorand-sdk/commit/7d0397d23fc14d80a087d8c809bb7b9babf3b7c8))
- **walletconnect:** fix `WalletConnectAccount` failing to assign Key for new session ([ccfd6db](https://github.com/CareBoo/unity-algorand-sdk/commit/ccfd6db4f66ee99c1b91157ac034f9b7ece227e0))
- **walletconnect:** fix `WalletConnectRpc.GetRandomId` throwing error when not in main thread ([133fbd6](https://github.com/CareBoo/unity-algorand-sdk/commit/133fbd6aa608f3b50e79cab1793697e8c304408f))
- **walletconnect:** fix deep linking in iOS ([997ad45](https://github.com/CareBoo/unity-algorand-sdk/commit/997ad455e6813d9dca377d027c4cba8f13c1aa9b))
- **walletconnect:** fix null reference err in `JsonRpcClient` ([72f8f1f](https://github.com/CareBoo/unity-algorand-sdk/commit/72f8f1fc481c3e6011418e7a5f91741b7effa691))
- **walletconnect:** fix null reference in `WebSocketExtensions` ([6f23d10](https://github.com/CareBoo/unity-algorand-sdk/commit/6f23d1008427e5b560e9f85d9cafbfd40b90d69b))
- **walletconnect:** fix random id range and parsing ([c35e2ed](https://github.com/CareBoo/unity-algorand-sdk/commit/c35e2edbf160ac7454b5a9a7fe7ec94e751b79b1))
- **webgl:** fix webgl errors being caused in player test builds ([f7ee641](https://github.com/CareBoo/unity-algorand-sdk/commit/f7ee6419df137363d5e5e2b17d9cf5b904a4f0db))

### Code Refactoring

- **abi:** mark ABI as Experimental ([86f2c25](https://github.com/CareBoo/unity-algorand-sdk/commit/86f2c25a2e5234624b922039c1c4287353fa6537))
- **editor:** move editor-specific utilities and windows to new sample ([a345cc2](https://github.com/CareBoo/unity-algorand-sdk/commit/a345cc2142628bb912d124ebf890c7ef3014ac8d))

### Features

- **abi:** add `ArgsArray`, making it easy to pass in `IAbiValue` params to AtomicTxn Builder ([523d765](https://github.com/CareBoo/unity-algorand-sdk/commit/523d765cc475878676990aaa7369219a56f6f1f2))
- **abi:** add `Decode` methods to ABI ([a83f1fe](https://github.com/CareBoo/unity-algorand-sdk/commit/a83f1fe38df6501c9941e496d31e28dbb37d059a))
- **abi:** add ABI support ([d43eed7](https://github.com/CareBoo/unity-algorand-sdk/commit/d43eed7ebbfa1e52210abb9d85153206a1504591)), closes [#132](https://github.com/CareBoo/unity-algorand-sdk/issues/132)
- **abi:** add abitype inspectors ([a48d710](https://github.com/CareBoo/unity-algorand-sdk/commit/a48d7106595b1166c483d09ac4cebdde424b9fd4))
- **abi:** add APIs to better create Tuples from ArgsArray ([5a109ed](https://github.com/CareBoo/unity-algorand-sdk/commit/5a109ed4c45a490ff2e3113342d88121f4b04894))
- **account:** add `IsAddressString` to `Address` ([a908586](https://github.com/CareBoo/unity-algorand-sdk/commit/a9085869a0d16737d1e3d6eb56c3fb7bc55b4a02))
- **accounts:** add `MinBalance` constructor that takes an `Algorand.Unity.Indexer.Account` result ([03dc71a](https://github.com/CareBoo/unity-algorand-sdk/commit/03dc71a8653a242a6a81d831db76122bc6299233))
- **algoapi:** create separate types for REST APIs and SDK ([#129](https://github.com/CareBoo/unity-algorand-sdk/issues/129)) ([0a97a11](https://github.com/CareBoo/unity-algorand-sdk/commit/0a97a11643e45a59f749fe14ec6e8d0e5b547cb2)), closes [#120](https://github.com/CareBoo/unity-algorand-sdk/issues/120)
- **algod:** add `Account` property to `Algod.AccountResponse` ([416685b](https://github.com/CareBoo/unity-algorand-sdk/commit/416685b2dc1bdd08c3a7b54eb1c495f5d27f9488))
- **algod:** add latest algod client features ([8873de7](https://github.com/CareBoo/unity-algorand-sdk/commit/8873de74d90e176b300e10d1efba119c90b62269))
- **algod:** add utility method `AlgodClient.WaitForConfirmation` to wait for Txn confirmation ([1e60a2d](https://github.com/CareBoo/unity-algorand-sdk/commit/1e60a2de6c02a5456646fcae915f1cb07952598f))
- **api:** add `AlgoApiException` that can be thrown from an `ErrorResponse` ([2eb8209](https://github.com/CareBoo/unity-algorand-sdk/commit/2eb8209386dc20b8f2cadb746a1c36db6453baa6))
- **api:** update `AlgoApiRequest.Sent` to be convertible to UniTask ([522d4c7](https://github.com/CareBoo/unity-algorand-sdk/commit/522d4c75d3bd1761f0a9e6920227bcd606b246ec))
- **atomic-txn:** add APIs for building, signing, and serializing Atomic Txns ([858ff9e](https://github.com/CareBoo/unity-algorand-sdk/commit/858ff9e0245a1de254f8e18beff62a8177943144)), closes [#131](https://github.com/CareBoo/unity-algorand-sdk/issues/131)
- **atomictxn:** add submitted and confirmed workflow to atomic transactions ([dd59ae6](https://github.com/CareBoo/unity-algorand-sdk/commit/dd59ae6641ece47d6daaa016e7768e1416875ed1))
- **blockchain:** add `AlgorandNetwork` enum for describing the different networks ([52594e6](https://github.com/CareBoo/unity-algorand-sdk/commit/52594e61cae6128763e2088c385e4b7e21fb377b))
- **endianness:** add new `FromBytesBigEndian` methods ([2623f43](https://github.com/CareBoo/unity-algorand-sdk/commit/2623f433abc385756be184ae6665ca0e12706f93))
- **signer:** update `ISigner` interface to be closer to signer interface in other SDKs ([a8fbe0b](https://github.com/CareBoo/unity-algorand-sdk/commit/a8fbe0bbbc7729b07beef1efc7e5d8757db0cd41)), closes [/github.com/algorand/py-algorand-sdk/blob/aba9f4ccf87d4e7f7d5d6d4826e38463b76da9b8/Algorand.Unity/atomic_transaction_composer.py#L558](https://github.com//github.com/algorand/py-algorand-sdk/blob/aba9f4ccf87d4e7f7d5d6d4826e38463b76da9b8/Algorand.Unity/atomic_transaction_composer.py/issues/L558) [#131](https://github.com/CareBoo/unity-algorand-sdk/issues/131)
- **transaction:** add `SignWith` and `SignWithAsync` API to `ITransaction` ([81aa758](https://github.com/CareBoo/unity-algorand-sdk/commit/81aa758eee039e6a774522ed3d9c9a9909ef9c0b))
- **util:** add `Optional<T>.Else` method ([2d6c26a](https://github.com/CareBoo/unity-algorand-sdk/commit/2d6c26a9d7182a5b61b42636c9d6727518b46a02))
- **walletconnect:** add `SessionData.Reinitialize` method ([0d172ac](https://github.com/CareBoo/unity-algorand-sdk/commit/0d172ac0a61f7cf15291cc0602fe7e62d3392845))
- **walletconnect:** add wallet connect support to UnityEditor ([#143](https://github.com/CareBoo/unity-algorand-sdk/issues/143)) ([60c13af](https://github.com/CareBoo/unity-algorand-sdk/commit/60c13af8bb06680f9add3e00a97a7c41428de2e0))
- **walletconnect:** expose `FormatUrlForDeepLink` API ([f26a7fd](https://github.com/CareBoo/unity-algorand-sdk/commit/f26a7fd200f8a68a3678817468d15df01cd927d4))
- **walletconnect:** expose `SessionData.GenKey` API ([6630652](https://github.com/CareBoo/unity-algorand-sdk/commit/6630652f34bc25905fa470ffe3485808370e341f))
- **walletconnect:** support chain ids in ARC-0025 ([32c518e](https://github.com/CareBoo/unity-algorand-sdk/commit/32c518eb6929866993a9fd306c477f571328551d))

### BREAKING CHANGES

- **abi:** `Algorand.Unity.Abi` renamed to `Algorand.Unity.Experimental.Abi`
- **editor:** Moved all `ScriptableObject` implementations to the `AssetCreation` sample.
- **walletconnect:** `AlgorandWalletConnectSession` flow has been changed to reflect new `JsonRpcClient`.

* `StartConnection` renamed to `Connect`
* `Disconnect` renamed to `DisconnectWallet`
* `WaitForConnectionApproval` renamed to `WaitForWalletApproval`
* `SavedSession` renamed to `SessionData`

- **signer:** Remove `ISigner` interfaces from `Algorand.Unity.Account` and completely change the signer
  API.
- **algoapi:** All APIs and return types from `AlgodClient`, `IndexerClient`, and `KmdClient` have changed. Additional breaking changes:

* `MinBalance(AccountInfo)` constructor has changed to take an `Algorand.Unity.Algod.Account` value instead.
* Remove `PrivateKey.SignTransaction`
* Rename `AppEvalDelta` -> `EvalDelta`
* Rename `AppStateDelta` -> `StateDelta`
* Rename `EvalDelta` -> `ValueDelta`
* Rename `EvalDeltaKeyValue` -> `ValueDeltaKeyValue`
* Remove readonly fields from all Transaction types.
* Rename `Multisig` -> `MultisigSig`

# [3.0.0-pre.14](https://github.com/CareBoo/unity-algorand-sdk/compare/v3.0.0-pre.13...v3.0.0-pre.14) (2022-07-16)

### Features

- **algod:** add `Account` property to `Algod.AccountResponse` ([416685b](https://github.com/CareBoo/unity-algorand-sdk/commit/416685b2dc1bdd08c3a7b54eb1c495f5d27f9488))
- **algod:** add latest algod client features ([8873de7](https://github.com/CareBoo/unity-algorand-sdk/commit/8873de74d90e176b300e10d1efba119c90b62269))

# [3.0.0-pre.13](https://github.com/CareBoo/unity-algorand-sdk/compare/v3.0.0-pre.12...v3.0.0-pre.13) (2022-07-16)

### Bug Fixes

- **walletconnect:** fix deep linking in iOS ([997ad45](https://github.com/CareBoo/unity-algorand-sdk/commit/997ad455e6813d9dca377d027c4cba8f13c1aa9b))

# [3.0.0-pre.12](https://github.com/CareBoo/unity-algorand-sdk/compare/v3.0.0-pre.11...v3.0.0-pre.12) (2022-07-14)

### Bug Fixes

- **abi:** fix Tuple not encoding dynamic types correctly ([be125d3](https://github.com/CareBoo/unity-algorand-sdk/commit/be125d3b7908a95d8a373f30aa5b081cb358267f))
- **kmd:** fix `KmdAccount` not renewing wallet handle tokens correctly ([1b0479d](https://github.com/CareBoo/unity-algorand-sdk/commit/1b0479dd2b54b0c0c4820018fd2d50f2440021ea))

# [3.0.0-pre.11](https://github.com/CareBoo/unity-algorand-sdk/compare/v3.0.0-pre.10...v3.0.0-pre.11) (2022-07-13)

### Features

- **walletconnect:** expose `FormatUrlForDeepLink` API ([f26a7fd](https://github.com/CareBoo/unity-algorand-sdk/commit/f26a7fd200f8a68a3678817468d15df01cd927d4))
- **walletconnect:** expose `SessionData.GenKey` API ([6630652](https://github.com/CareBoo/unity-algorand-sdk/commit/6630652f34bc25905fa470ffe3485808370e341f))
- **walletconnect:** support chain ids in ARC-0025 ([32c518e](https://github.com/CareBoo/unity-algorand-sdk/commit/32c518eb6929866993a9fd306c477f571328551d))

# [3.0.0-pre.10](https://github.com/CareBoo/unity-algorand-sdk/compare/v3.0.0-pre.9...v3.0.0-pre.10) (2022-07-11)

### Bug Fixes

- **collections:** fix obsolete API in most recent Unity Collections package ([cf05ec9](https://github.com/CareBoo/unity-algorand-sdk/commit/cf05ec9bff584aa7dab37e835427e004a52d5d2d))
- **lowlevel:** fix `NativeListOfList` not correctly assigning indices with `IIndexable<>` types ([aaa3b15](https://github.com/CareBoo/unity-algorand-sdk/commit/aaa3b15ddf906eb1cde4530a0261562584dd136b))
- **msgpack:** fix MessagePackWriter.Bytes issue when writing empty byte array ([eef81de](https://github.com/CareBoo/unity-algorand-sdk/commit/eef81de2b80c74a6b9eac132ec33e6c7528daaf4))
- **walletconnect:** fix `WalletConnectAccount` failing to assign Key for new session ([ccfd6db](https://github.com/CareBoo/unity-algorand-sdk/commit/ccfd6db4f66ee99c1b91157ac034f9b7ece227e0))

### Features

- **abi:** add `Decode` methods to ABI ([a83f1fe](https://github.com/CareBoo/unity-algorand-sdk/commit/a83f1fe38df6501c9941e496d31e28dbb37d059a))
- **account:** add `IsAddressString` to `Address` ([a908586](https://github.com/CareBoo/unity-algorand-sdk/commit/a9085869a0d16737d1e3d6eb56c3fb7bc55b4a02))
- **api:** update `AlgoApiRequest.Sent` to be convertible to UniTask ([522d4c7](https://github.com/CareBoo/unity-algorand-sdk/commit/522d4c75d3bd1761f0a9e6920227bcd606b246ec))
- **atomictxn:** add submitted and confirmed workflow to atomic transactions ([dd59ae6](https://github.com/CareBoo/unity-algorand-sdk/commit/dd59ae6641ece47d6daaa016e7768e1416875ed1))
- **endianness:** add new `FromBytesBigEndian` methods ([2623f43](https://github.com/CareBoo/unity-algorand-sdk/commit/2623f433abc385756be184ae6665ca0e12706f93))

# [3.0.0-pre.9](https://github.com/CareBoo/unity-algorand-sdk/compare/v3.0.0-pre.8...v3.0.0-pre.9) (2022-06-20)

### Bug Fixes

- **algod:** fix `AlgodClient.WaitForConfirmation` not using correct wait time ([169be0b](https://github.com/CareBoo/unity-algorand-sdk/commit/169be0b4745fdf954a2c6cacc8c6b6deabd51b36))
- **json:** fix strings failing to be read on `null` ([dac58b7](https://github.com/CareBoo/unity-algorand-sdk/commit/dac58b7ee873a5443080429faad3efcfd6cff727))
- **webgl:** fix webgl errors being caused in player test builds ([f7ee641](https://github.com/CareBoo/unity-algorand-sdk/commit/f7ee6419df137363d5e5e2b17d9cf5b904a4f0db))

### Code Refactoring

- **abi:** mark ABI as Experimental ([86f2c25](https://github.com/CareBoo/unity-algorand-sdk/commit/86f2c25a2e5234624b922039c1c4287353fa6537))

### Features

- **abi:** add APIs to better create Tuples from ArgsArray ([5a109ed](https://github.com/CareBoo/unity-algorand-sdk/commit/5a109ed4c45a490ff2e3113342d88121f4b04894))
- **api:** add `AlgoApiException` that can be thrown from an `ErrorResponse` ([2eb8209](https://github.com/CareBoo/unity-algorand-sdk/commit/2eb8209386dc20b8f2cadb746a1c36db6453baa6))

### BREAKING CHANGES

- **abi:** `Algorand.Unity.Abi` renamed to `Algorand.Unity.Experimental.Abi`

# [3.0.0-pre.8](https://github.com/CareBoo/unity-algorand-sdk/compare/v3.0.0-pre.7...v3.0.0-pre.8) (2022-06-14)

### Features

- **abi:** add `ArgsArray`, making it easy to pass in `IAbiValue` params to AtomicTxn Builder ([523d765](https://github.com/CareBoo/unity-algorand-sdk/commit/523d765cc475878676990aaa7369219a56f6f1f2))
- **transaction:** add `SignWith` and `SignWithAsync` API to `ITransaction` ([81aa758](https://github.com/CareBoo/unity-algorand-sdk/commit/81aa758eee039e6a774522ed3d9c9a9909ef9c0b))

# [3.0.0-pre.7](https://github.com/CareBoo/unity-algorand-sdk/compare/v3.0.0-pre.6...v3.0.0-pre.7) (2022-06-13)

### Features

- **accounts:** add `MinBalance` constructor that takes an `Algorand.Unity.Indexer.Account` result ([03dc71a](https://github.com/CareBoo/unity-algorand-sdk/commit/03dc71a8653a242a6a81d831db76122bc6299233))

# [3.0.0-pre.6](https://github.com/CareBoo/unity-algorand-sdk/compare/v3.0.0-pre.5...v3.0.0-pre.6) (2022-06-12)

### Bug Fixes

- **inspector:** fix gaps between label and property in custom property drawers ([2ad65da](https://github.com/CareBoo/unity-algorand-sdk/commit/2ad65da3a44b3ac422be11be8483b8c1b931290c))
- **json:** fix `JsonReader.ReadNumber` for integers when given fraction with exponent ([8bf0758](https://github.com/CareBoo/unity-algorand-sdk/commit/8bf0758209d451f5310e9d64fda169758ad0f521))
- **walletconnect:** `WalletConnectAccount.SignTxns` can now be done without calling `BeginSession` ([84bcba9](https://github.com/CareBoo/unity-algorand-sdk/commit/84bcba94c4ceab8753cfdce7addfa80bd4ac91d1))
- **walletconnect:** add checks to `AlgorandWalletConnectSession(SessionData)` ([7d0397d](https://github.com/CareBoo/unity-algorand-sdk/commit/7d0397d23fc14d80a087d8c809bb7b9babf3b7c8))
- **walletconnect:** fix `WalletConnectRpc.GetRandomId` throwing error when not in main thread ([133fbd6](https://github.com/CareBoo/unity-algorand-sdk/commit/133fbd6aa608f3b50e79cab1793697e8c304408f))
- **walletconnect:** fix null reference err in `JsonRpcClient` ([72f8f1f](https://github.com/CareBoo/unity-algorand-sdk/commit/72f8f1fc481c3e6011418e7a5f91741b7effa691))
- **walletconnect:** fix null reference in `WebSocketExtensions` ([6f23d10](https://github.com/CareBoo/unity-algorand-sdk/commit/6f23d1008427e5b560e9f85d9cafbfd40b90d69b))
- **walletconnect:** fix random id range and parsing ([c35e2ed](https://github.com/CareBoo/unity-algorand-sdk/commit/c35e2edbf160ac7454b5a9a7fe7ec94e751b79b1))

### Code Refactoring

- **editor:** move editor-specific utilities and windows to new sample ([a345cc2](https://github.com/CareBoo/unity-algorand-sdk/commit/a345cc2142628bb912d124ebf890c7ef3014ac8d))

### Features

- **algod:** add utility method `AlgodClient.WaitForConfirmation` to wait for Txn confirmation ([1e60a2d](https://github.com/CareBoo/unity-algorand-sdk/commit/1e60a2de6c02a5456646fcae915f1cb07952598f))
- **blockchain:** add `AlgorandNetwork` enum for describing the different networks ([52594e6](https://github.com/CareBoo/unity-algorand-sdk/commit/52594e61cae6128763e2088c385e4b7e21fb377b))
- **util:** add `Optional<T>.Else` method ([2d6c26a](https://github.com/CareBoo/unity-algorand-sdk/commit/2d6c26a9d7182a5b61b42636c9d6727518b46a02))
- **walletconnect:** add `SessionData.Reinitialize` method ([0d172ac](https://github.com/CareBoo/unity-algorand-sdk/commit/0d172ac0a61f7cf15291cc0602fe7e62d3392845))

### BREAKING CHANGES

- **editor:** Moved all `ScriptableObject` implementations to the `AssetCreation` sample.

# [3.0.0-pre.5](https://github.com/CareBoo/unity-algorand-sdk/compare/v3.0.0-pre.4...v3.0.0-pre.5) (2022-06-05)

### Features

- **walletconnect:** add wallet connect support to UnityEditor ([#143](https://github.com/CareBoo/unity-algorand-sdk/issues/143)) ([60c13af](https://github.com/CareBoo/unity-algorand-sdk/commit/60c13af8bb06680f9add3e00a97a7c41428de2e0))

### BREAKING CHANGES

- **walletconnect:** `AlgorandWalletConnectSession` flow has been changed to reflect new `JsonRpcClient`.

* `StartConnection` renamed to `Connect`
* `Disconnect` renamed to `DisconnectWallet`
* `WaitForConnectionApproval` renamed to `WaitForWalletApproval`
* `SavedSession` renamed to `SessionData`

# [3.0.0-pre.4](https://github.com/CareBoo/unity-algorand-sdk/compare/v3.0.0-pre.3...v3.0.0-pre.4) (2022-06-02)

### Features

- **abi:** add abitype inspectors ([a48d710](https://github.com/CareBoo/unity-algorand-sdk/commit/a48d7106595b1166c483d09ac4cebdde424b9fd4))

# [3.0.0-pre.3](https://github.com/CareBoo/unity-algorand-sdk/compare/v3.0.0-pre.2...v3.0.0-pre.3) (2022-05-28)

### Features

- **abi:** add ABI support ([d43eed7](https://github.com/CareBoo/unity-algorand-sdk/commit/d43eed7ebbfa1e52210abb9d85153206a1504591)), closes [#132](https://github.com/CareBoo/unity-algorand-sdk/issues/132)

# [3.0.0-pre.2](https://github.com/CareBoo/unity-algorand-sdk/compare/v3.0.0-pre.1...v3.0.0-pre.2) (2022-04-12)

### Features

- **atomic-txn:** add APIs for building, signing, and serializing Atomic Txns ([858ff9e](https://github.com/CareBoo/unity-algorand-sdk/commit/858ff9e0245a1de254f8e18beff62a8177943144)), closes [#131](https://github.com/CareBoo/unity-algorand-sdk/issues/131)
- **signer:** update `ISigner` interface to be closer to signer interface in other SDKs ([a8fbe0b](https://github.com/CareBoo/unity-algorand-sdk/commit/a8fbe0bbbc7729b07beef1efc7e5d8757db0cd41)), closes [/github.com/algorand/py-algorand-sdk/blob/aba9f4ccf87d4e7f7d5d6d4826e38463b76da9b8/Algorand.Unity/atomic_transaction_composer.py#L558](https://github.com//github.com/algorand/py-algorand-sdk/blob/aba9f4ccf87d4e7f7d5d6d4826e38463b76da9b8/Algorand.Unity/atomic_transaction_composer.py/issues/L558) [#131](https://github.com/CareBoo/unity-algorand-sdk/issues/131)

### BREAKING CHANGES

- **signer:** Remove `ISigner` interfaces from `Algorand.Unity.Account` and completely change the signer
  API.

# [3.0.0-pre.1](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.3.0-pre.4...v3.0.0-pre.1) (2022-04-03)

### Features

- **algoapi:** create separate types for REST APIs and SDK ([#129](https://github.com/CareBoo/unity-algorand-sdk/issues/129)) ([0a97a11](https://github.com/CareBoo/unity-algorand-sdk/commit/0a97a11643e45a59f749fe14ec6e8d0e5b547cb2)), closes [#120](https://github.com/CareBoo/unity-algorand-sdk/issues/120)

### BREAKING CHANGES

- **algoapi:** All APIs and return types from `AlgodClient`, `IndexerClient`, and `KmdClient` have changed. Additional breaking changes:

* `MinBalance(AccountInfo)` constructor has changed to take an `Algorand.Unity.Algod.Account` value instead.
* Remove `PrivateKey.SignTransaction`
* Rename `AppEvalDelta` -> `EvalDelta`
* Rename `AppStateDelta` -> `StateDelta`
* Rename `EvalDelta` -> `ValueDelta`
* Rename `EvalDeltaKeyValue` -> `ValueDeltaKeyValue`
* Remove readonly fields from all Transaction types.
* Rename `Multisig` -> `MultisigSig`

# [2.3.0-pre.4](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.3.0-pre.3...v2.3.0-pre.4) (2022-03-25)

### Features

- **account:** add fields for total assets/accounts opted-in/created ([3ea7882](https://github.com/CareBoo/unity-algorand-sdk/commit/3ea788294950d631e227ecad1af8129eb7d09bca))

# [2.3.0-pre.3](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.3.0-pre.2...v2.3.0-pre.3) (2022-03-25)

### Features

- **errors:** serializer read errors now print the full message they're deserializing ([b67db2a](https://github.com/CareBoo/unity-algorand-sdk/commit/b67db2a0dd52bd3bfacc7d4b66a242beafcca950))
- **formatter:** add `isStrict` to `AlgoApiObjectFormatter` ([d8467c6](https://github.com/CareBoo/unity-algorand-sdk/commit/d8467c632172fb41e207cc13b76d45717665f26a))

# [2.3.0-pre.2](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.3.0-pre.1...v2.3.0-pre.2) (2022-03-25)

### Features

- **transactiongroup:** add generic methods for constructing `TransactionGroup` ([5e4b6aa](https://github.com/CareBoo/unity-algorand-sdk/commit/5e4b6aac8a9fd42e9ffe27a73623d3d81e5d848d)), closes [#126](https://github.com/CareBoo/unity-algorand-sdk/issues/126)

# [2.3.0-pre.1](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.2.0...v2.3.0-pre.1) (2022-03-24)

### Features

- **indexer:** add new `IIndexerResponse<T>` and `IPaginatedIndexerResponse<T>` interfaces ([3b3abda](https://github.com/CareBoo/unity-algorand-sdk/commit/3b3abda9bad5e1d8b6964e07b0bdd8533b8aa5e4))
- **transaction:** add Indexer TransactionApplication model ([99f63fa](https://github.com/CareBoo/unity-algorand-sdk/commit/99f63fa443d4013bcda218cae45c9d1d0ed4f170))

# [2.2.0](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.1.1...v2.2.0) (2022-03-23)

### Bug Fixes

- **editor:** fix Tooltip compilation issue in unity 2020.3 ([6b71300](https://github.com/CareBoo/unity-algorand-sdk/commit/6b713003e79efdbd124820bfdc5fd236c62ddba0))

### Features

- **account:** add `IAccount`, `ISigner`, and `IAsyncSigner` interfaces ([19f139d](https://github.com/CareBoo/unity-algorand-sdk/commit/19f139db99e03e389d7f8abfad45e1bdb3b0667d)), closes [#82](https://github.com/CareBoo/unity-algorand-sdk/issues/82)
- **account:** add `MicroAlgos` wrapper class ([57a89e6](https://github.com/CareBoo/unity-algorand-sdk/commit/57a89e61fa434a58f5034194c5630dfc2332d586))
- **accounts:** add `MinBalance` to allow developers to estimate min balances ([856c11a](https://github.com/CareBoo/unity-algorand-sdk/commit/856c11ae3ae3339caf33a8f21bcca52a8d8e69d4)), closes [#104](https://github.com/CareBoo/unity-algorand-sdk/issues/104)
- **application:** add `AppIndex` type ([458392b](https://github.com/CareBoo/unity-algorand-sdk/commit/458392b2f0ebb552d48fc84f92dff8e6145f7198))
- **asset:** add `AssetIndex` type ([2c44d55](https://github.com/CareBoo/unity-algorand-sdk/commit/2c44d555680c875fb8767b452027f49768f16d67))
- **teal:** add conversion for supported types to `CompiledTeal` ([#112](https://github.com/CareBoo/unity-algorand-sdk/issues/112)) ([5a279d2](https://github.com/CareBoo/unity-algorand-sdk/commit/5a279d297df5225a60efb1548a5bd6467f961f40)), closes [#105](https://github.com/CareBoo/unity-algorand-sdk/issues/105)
- **transaction:** add ability to fully deserialize `BlockTransaction` ([f32da2f](https://github.com/CareBoo/unity-algorand-sdk/commit/f32da2f53938b65bd7e3bdcd19dc567f2800100d)), closes [#108](https://github.com/CareBoo/unity-algorand-sdk/issues/108)
- **transaction:** refactor signed transaction to use `[AlgoApiObject]` ([8ff034d](https://github.com/CareBoo/unity-algorand-sdk/commit/8ff034df45af46469595cf04e2f16cf327b24826)), closes [#113](https://github.com/CareBoo/unity-algorand-sdk/issues/113) [#114](https://github.com/CareBoo/unity-algorand-sdk/issues/114)

# [2.2.0-pre.4](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.2.0-pre.3...v2.2.0-pre.4) (2022-03-22)

### Features

- **account:** add `IAccount`, `ISigner`, and `IAsyncSigner` interfaces ([19f139d](https://github.com/CareBoo/unity-algorand-sdk/commit/19f139db99e03e389d7f8abfad45e1bdb3b0667d)), closes [#82](https://github.com/CareBoo/unity-algorand-sdk/issues/82)

# [2.2.0-pre.3](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.2.0-pre.2...v2.2.0-pre.3) (2022-03-21)

### Bug Fixes

- **editor:** fix Tooltip compilation issue in unity 2020.3 ([6b71300](https://github.com/CareBoo/unity-algorand-sdk/commit/6b713003e79efdbd124820bfdc5fd236c62ddba0))

### Features

- **account:** add `MicroAlgos` wrapper class ([57a89e6](https://github.com/CareBoo/unity-algorand-sdk/commit/57a89e61fa434a58f5034194c5630dfc2332d586))
- **application:** add `AppIndex` type ([458392b](https://github.com/CareBoo/unity-algorand-sdk/commit/458392b2f0ebb552d48fc84f92dff8e6145f7198))
- **asset:** add `AssetIndex` type ([2c44d55](https://github.com/CareBoo/unity-algorand-sdk/commit/2c44d555680c875fb8767b452027f49768f16d67))
- **transaction:** add ability to fully deserialize `BlockTransaction` ([f32da2f](https://github.com/CareBoo/unity-algorand-sdk/commit/f32da2f53938b65bd7e3bdcd19dc567f2800100d)), closes [#108](https://github.com/CareBoo/unity-algorand-sdk/issues/108)
- **transaction:** refactor signed transaction to use `[AlgoApiObject]` ([8ff034d](https://github.com/CareBoo/unity-algorand-sdk/commit/8ff034df45af46469595cf04e2f16cf327b24826)), closes [#113](https://github.com/CareBoo/unity-algorand-sdk/issues/113) [#114](https://github.com/CareBoo/unity-algorand-sdk/issues/114)

# [2.2.0-pre.2](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.2.0-pre.1...v2.2.0-pre.2) (2022-03-20)

### Features

- **teal:** add conversion for supported types to `CompiledTeal` ([#112](https://github.com/CareBoo/unity-algorand-sdk/issues/112)) ([5a279d2](https://github.com/CareBoo/unity-algorand-sdk/commit/5a279d297df5225a60efb1548a5bd6467f961f40)), closes [#105](https://github.com/CareBoo/unity-algorand-sdk/issues/105)

# [2.2.0-pre.1](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.1.1-pre.2...v2.2.0-pre.1) (2022-03-20)

### Features

- **accounts:** add `MinBalance` to allow developers to estimate min balances ([856c11a](https://github.com/CareBoo/unity-algorand-sdk/commit/856c11ae3ae3339caf33a8f21bcca52a8d8e69d4)), closes [#104](https://github.com/CareBoo/unity-algorand-sdk/issues/104)

## [2.1.1](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.1.0...v2.1.1) (2022-03-20)

### Bug Fixes

- **appcalltxn:** fix AppArguments not in correct format ([7845c79](https://github.com/CareBoo/unity-algorand-sdk/commit/7845c7979d3c925db953dc36db4d0647ae8c5e39)), closes [#96](https://github.com/CareBoo/unity-algorand-sdk/issues/96)
- **block:** fix `BlockTransaction` field names ([ead4f5d](https://github.com/CareBoo/unity-algorand-sdk/commit/ead4f5d08aa954e9db7930ef73428b8253c64699))
- **formatters:** fix `EvalDelta` not deserializing from msgpack properly ([827e6d1](https://github.com/CareBoo/unity-algorand-sdk/commit/827e6d1e5fe7f0e89e5f9043890943a7ae6c0140)), closes [#107](https://github.com/CareBoo/unity-algorand-sdk/issues/107)
- **walletconnect:** fix issue where WalletTransaction.New requires unnecessary type constraints ([09ce3f4](https://github.com/CareBoo/unity-algorand-sdk/commit/09ce3f4b48f60fc6b544349c2dc6da8894521fcf))

## [2.1.1-pre.2](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.1.1-pre.1...v2.1.1-pre.2) (2022-03-20)

### Bug Fixes

- **block:** fix `BlockTransaction` field names ([ead4f5d](https://github.com/CareBoo/unity-algorand-sdk/commit/ead4f5d08aa954e9db7930ef73428b8253c64699))
- **formatters:** fix `EvalDelta` not deserializing from msgpack properly ([827e6d1](https://github.com/CareBoo/unity-algorand-sdk/commit/827e6d1e5fe7f0e89e5f9043890943a7ae6c0140)), closes [#107](https://github.com/CareBoo/unity-algorand-sdk/issues/107)

## [2.1.1-pre.1](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.1.0...v2.1.1-pre.1) (2022-03-19)

### Bug Fixes

- **appcalltxn:** fix AppArguments not in correct format ([7845c79](https://github.com/CareBoo/unity-algorand-sdk/commit/7845c7979d3c925db953dc36db4d0647ae8c5e39)), closes [#96](https://github.com/CareBoo/unity-algorand-sdk/issues/96)
- **walletconnect:** fix issue where WalletTransaction.New requires unnecessary type constraints ([09ce3f4](https://github.com/CareBoo/unity-algorand-sdk/commit/09ce3f4b48f60fc6b544349c2dc6da8894521fcf))

# [2.1.0-pre.12](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.1.0-pre.11...v2.1.0-pre.12) (2022-03-19)

### Bug Fixes

- **appcalltxn:** fix AppArguments not in correct format ([7845c79](https://github.com/CareBoo/unity-algorand-sdk/commit/7845c7979d3c925db953dc36db4d0647ae8c5e39)), closes [#96](https://github.com/CareBoo/unity-algorand-sdk/issues/96)
- **walletconnect:** fix issue where WalletTransaction.New requires unnecessary type constraints ([09ce3f4](https://github.com/CareBoo/unity-algorand-sdk/commit/09ce3f4b48f60fc6b544349c2dc6da8894521fcf))

# [2.1.0](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.0.0...v2.1.0) (2022-03-17)

### Bug Fixes

- **algod:** fix PendingTransaction fields from messagepack ([6dbf0b3](https://github.com/CareBoo/unity-algorand-sdk/commit/6dbf0b3c99f7977ce87458e51ea8a9f87d9f9075)), closes [#97](https://github.com/CareBoo/unity-algorand-sdk/issues/97)
- **build:** fix build not using the latest version of the release ([e8ee758](https://github.com/CareBoo/unity-algorand-sdk/commit/e8ee7581a4496805a60e1af0aea452f9c69969a8))
- **docs:** update readme with png image ([a84c298](https://github.com/CareBoo/unity-algorand-sdk/commit/a84c298a62503fc14f2143d6d2808540ae9d06eb))
- fix missing meta files from package ([a4037c9](https://github.com/CareBoo/unity-algorand-sdk/commit/a4037c94d006b3ede15348868fbb8b586b4e84b6))
- **formatter:** make sure to skip unknown keys ([a9115af](https://github.com/CareBoo/unity-algorand-sdk/commit/a9115af470ce07e373506a9cc43edd028c96b636))
- **formatters:** add explicit formatters for enums ([58c4475](https://github.com/CareBoo/unity-algorand-sdk/commit/58c447540eb117eb507e8147fb58b563506da959)), closes [#99](https://github.com/CareBoo/unity-algorand-sdk/issues/99)
- **formatters:** update AlgoApiFormatter to warn instead of error if key cannot be found ([f727135](https://github.com/CareBoo/unity-algorand-sdk/commit/f727135db83deb6aebd010da31ea3e1df91a9b94)), closes [#98](https://github.com/CareBoo/unity-algorand-sdk/issues/98)
- **kmd:** fix multisig serialization ([a48e109](https://github.com/CareBoo/unity-algorand-sdk/commit/a48e109b305f0c607e626eae64bfa6610c191503))
- **readme:** fix missing image in readme ([a2fb33c](https://github.com/CareBoo/unity-algorand-sdk/commit/a2fb33ce7bd1ea760e098f36e809a5ec4d9ea3ab))
- **readme:** remove unnecessary xml tag ([2f87b1a](https://github.com/CareBoo/unity-algorand-sdk/commit/2f87b1ab42c0854dedbe03da00a78f2a6fefdb87))
- **samples:** fix compile error with samples ([c7c9a0d](https://github.com/CareBoo/unity-algorand-sdk/commit/c7c9a0de2f7596a1a2a31b570ce8ee73d512d63b))
- **samples:** update YourFirstTransaction sample ([20cabf7](https://github.com/CareBoo/unity-algorand-sdk/commit/20cabf70da8cbf47096c5586cb8deb53b03ae233))
- **serialization:** fix initialization of AlgoApiFormatterLookup ([675b1d9](https://github.com/CareBoo/unity-algorand-sdk/commit/675b1d90a911629597a96127d9c62662eb48aba3))
- **serialization:** set formatters to automatically add array formatter if it doesn't exist ([5692372](https://github.com/CareBoo/unity-algorand-sdk/commit/56923722974f710fa7583fcbf48dfa251f85dd7d))
- **websocket:** fix TLS failure in websocket-sharp for Unity 2020.3 ([bdaa92e](https://github.com/CareBoo/unity-algorand-sdk/commit/bdaa92e4f55aac89c2abf897a28bced7fc1aac65)), closes [#101](https://github.com/CareBoo/unity-algorand-sdk/issues/101)

### Features

- **api:** add min-balance field ([7f0873b](https://github.com/CareBoo/unity-algorand-sdk/commit/7f0873bbfddabf80b36e67cb85a916fb7270835a))
- **transaction:** add `PrivateKey.SignTransaction` method ([3d74336](https://github.com/CareBoo/unity-algorand-sdk/commit/3d74336aee240bb8357cea27b26c33cb537f3eca))
- **walletconnect:** add `LaunchApp[...]` to `AppEntry` ([f822855](https://github.com/CareBoo/unity-algorand-sdk/commit/f822855e1ab81a8a768fac359e65f87041e90de2))
- **walletconnect:** add supported wallets list ([50c6575](https://github.com/CareBoo/unity-algorand-sdk/commit/50c657580ccf5a1cb751d78cfb14201c4f8ac9fc))

# [2.1.0-pre.11](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.1.0-pre.10...v2.1.0-pre.11) (2022-03-17)

### Features

- **walletconnect:** add `LaunchApp[...]` to `AppEntry` ([f822855](https://github.com/CareBoo/unity-algorand-sdk/commit/f822855e1ab81a8a768fac359e65f87041e90de2))

# [2.1.0-pre.10](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.1.0-pre.9...v2.1.0-pre.10) (2022-03-17)

### Bug Fixes

- **formatter:** make sure to skip unknown keys ([a9115af](https://github.com/CareBoo/unity-algorand-sdk/commit/a9115af470ce07e373506a9cc43edd028c96b636))
- **kmd:** fix multisig serialization ([a48e109](https://github.com/CareBoo/unity-algorand-sdk/commit/a48e109b305f0c607e626eae64bfa6610c191503))

# [2.1.0-pre.9](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.1.0-pre.8...v2.1.0-pre.9) (2022-03-17)

### Bug Fixes

- fix missing meta files from package ([a4037c9](https://github.com/CareBoo/unity-algorand-sdk/commit/a4037c94d006b3ede15348868fbb8b586b4e84b6))
- **websocket:** fix TLS failure in websocket-sharp for Unity 2020.3 ([bdaa92e](https://github.com/CareBoo/unity-algorand-sdk/commit/bdaa92e4f55aac89c2abf897a28bced7fc1aac65)), closes [#101](https://github.com/CareBoo/unity-algorand-sdk/issues/101)

# [2.1.0-pre.8](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.1.0-pre.7...v2.1.0-pre.8) (2022-03-16)

### Bug Fixes

- **build:** fix build not using the latest version of the release ([e8ee758](https://github.com/CareBoo/unity-algorand-sdk/commit/e8ee7581a4496805a60e1af0aea452f9c69969a8))

# [2.1.0-pre.7](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.1.0-pre.6...v2.1.0-pre.7) (2022-03-16)

### Bug Fixes

- **algod:** fix PendingTransaction fields from messagepack ([6dbf0b3](https://github.com/CareBoo/unity-algorand-sdk/commit/6dbf0b3c99f7977ce87458e51ea8a9f87d9f9075)), closes [#97](https://github.com/CareBoo/unity-algorand-sdk/issues/97)
- **formatters:** add explicit formatters for enums ([58c4475](https://github.com/CareBoo/unity-algorand-sdk/commit/58c447540eb117eb507e8147fb58b563506da959)), closes [#99](https://github.com/CareBoo/unity-algorand-sdk/issues/99)
- **formatters:** update AlgoApiFormatter to warn instead of error if key cannot be found ([f727135](https://github.com/CareBoo/unity-algorand-sdk/commit/f727135db83deb6aebd010da31ea3e1df91a9b94)), closes [#98](https://github.com/CareBoo/unity-algorand-sdk/issues/98)
- **samples:** fix compile error with samples ([c7c9a0d](https://github.com/CareBoo/unity-algorand-sdk/commit/c7c9a0de2f7596a1a2a31b570ce8ee73d512d63b))

# [2.1.0-pre.6](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.1.0-pre.5...v2.1.0-pre.6) (2022-03-12)

### Bug Fixes

- **docs:** update readme with png image ([a84c298](https://github.com/CareBoo/unity-algorand-sdk/commit/a84c298a62503fc14f2143d6d2808540ae9d06eb))

# [2.1.0-pre.5](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.1.0-pre.4...v2.1.0-pre.5) (2022-03-12)

### Bug Fixes

- **readme:** remove unnecessary xml tag ([2f87b1a](https://github.com/CareBoo/unity-algorand-sdk/commit/2f87b1ab42c0854dedbe03da00a78f2a6fefdb87))

# [2.1.0-pre.4](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.1.0-pre.3...v2.1.0-pre.4) (2022-03-12)

### Bug Fixes

- **samples:** update YourFirstTransaction sample ([20cabf7](https://github.com/CareBoo/unity-algorand-sdk/commit/20cabf70da8cbf47096c5586cb8deb53b03ae233))

# [2.1.0-pre.3](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.1.0-pre.2...v2.1.0-pre.3) (2022-03-11)

### Bug Fixes

- **serialization:** fix initialization of AlgoApiFormatterLookup ([675b1d9](https://github.com/CareBoo/unity-algorand-sdk/commit/675b1d90a911629597a96127d9c62662eb48aba3))
- **serialization:** set formatters to automatically add array formatter if it doesn't exist ([5692372](https://github.com/CareBoo/unity-algorand-sdk/commit/56923722974f710fa7583fcbf48dfa251f85dd7d))

### Features

- **api:** add min-balance field ([7f0873b](https://github.com/CareBoo/unity-algorand-sdk/commit/7f0873bbfddabf80b36e67cb85a916fb7270835a))
- **walletconnect:** add supported wallets list ([50c6575](https://github.com/CareBoo/unity-algorand-sdk/commit/50c657580ccf5a1cb751d78cfb14201c4f8ac9fc))

# [2.1.0-pre.2](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.1.0-pre.1...v2.1.0-pre.2) (2022-03-06)

### Bug Fixes

- **readme:** fix missing image in readme ([a2fb33c](https://github.com/CareBoo/unity-algorand-sdk/commit/a2fb33ce7bd1ea760e098f36e809a5ec4d9ea3ab))

# [2.1.0-pre.1](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.0.0...v2.1.0-pre.1) (2022-03-06)

### Features

- **transaction:** add `PrivateKey.SignTransaction` method ([3d74336](https://github.com/CareBoo/unity-algorand-sdk/commit/3d74336aee240bb8357cea27b26c33cb537f3eca))

# [2.0.0](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.3.0...v2.0.0) (2022-03-05)

### Bug Fixes

- **qrcode:** replace zxing dll with its source code ([766c83e](https://github.com/CareBoo/unity-algorand-sdk/commit/766c83eae5ee0530b9bdd50aa7c337d8f5b64eb4)), closes [#84](https://github.com/CareBoo/unity-algorand-sdk/issues/84)
- **walletconnect:** fix compile issue for unity 2020 ([e26836a](https://github.com/CareBoo/unity-algorand-sdk/commit/e26836a3265c04f9908d813080c8c7f915891aeb))
- **walletconnect:** fix issue where new WalletConnect assembly was not compiling in Unity 2020.3 ([0a1ec26](https://github.com/CareBoo/unity-algorand-sdk/commit/0a1ec269d60e6a8d7cec50ea8496b9539b78916b))
- **websocket:** regenerate guids in `Algorand.Unity.WebSocket` assembly ([3cb3180](https://github.com/CareBoo/unity-algorand-sdk/commit/3cb31803ad00da8239fc077033931badbcea3676)), closes [#83](https://github.com/CareBoo/unity-algorand-sdk/issues/83)

### Code Refactoring

- **assemblies:** move libraries to their own assemblies to simplify things ([0b17ea1](https://github.com/CareBoo/unity-algorand-sdk/commit/0b17ea1758dd53960a82841c4ff3caa130ab2f09))
- **collections:** move `Unity.Collections` extensions into their own assembly ([8d217fc](https://github.com/CareBoo/unity-algorand-sdk/commit/8d217fc9823d427e7b93fff2213a850c84d8ae4d))
- move json, messagepack, and encoding logic into their own assemblies ([3d76a4b](https://github.com/CareBoo/unity-algorand-sdk/commit/3d76a4b551c0ca671216bb520c79a09482eca410))
- **walletconnect:** move WalletConnect code to its own assembly ([e6668cd](https://github.com/CareBoo/unity-algorand-sdk/commit/e6668cd68b99f81fdd8afedfcae35141e70b2284))
- **walletconnect:** remove unused enum `WalletConnectError` ([6df5079](https://github.com/CareBoo/unity-algorand-sdk/commit/6df5079fa71d57fbab6dd2bd9f4320758ff349a0))
- **websocket:** change namespace `Netcode.Transports.WebSocket` -> `Algorand.Unity.WebSocket` ([ddb1357](https://github.com/CareBoo/unity-algorand-sdk/commit/ddb1357acf39c14f47b940998bd51268d70bac41))
- **websocket:** move websocket code to its own assembly definition ([8ff6738](https://github.com/CareBoo/unity-algorand-sdk/commit/8ff6738942b65fe25b27218b80f8cdc630a698d8))

### Features

- **accounts:** add `AccountInfo.EstimateMinBalance` ([51d471b](https://github.com/CareBoo/unity-algorand-sdk/commit/51d471bf2991fd7c406ff3dcbac140b8b8085f38))
- **algod:** add overloads for `IAlgodClient.SendTransactions` that allow sending raw msgpack ([4befeeb](https://github.com/CareBoo/unity-algorand-sdk/commit/4befeebdb8f1bc635ed35344eaa8a2d6a762cae6))
- **api:** add `statusCode` deserialization field to `ErrorResponseFormatter` ([3f69600](https://github.com/CareBoo/unity-algorand-sdk/commit/3f696004496166a1c767e8a3887a3294209ef2b9))
- **api:** add support for `CancellationToken` and `IProgress<float>` ([6ca2f39](https://github.com/CareBoo/unity-algorand-sdk/commit/6ca2f395c072f97d5f32546feac0b8cec6c64ac9))
- **dependencies:** upgrade unity collections to 1.1.0 ([88308be](https://github.com/CareBoo/unity-algorand-sdk/commit/88308be43efed0ab75c8db8942fad4d077fe4b66))
- **lowlevel:** add `NativeArrayUtil.ConcatAll` ([10644db](https://github.com/CareBoo/unity-algorand-sdk/commit/10644db4c6e6a5256a3ffd2ea7a1441f0ac4499c))
- **models:** add implicit operators for either value type in an either ([84cfbcf](https://github.com/CareBoo/unity-algorand-sdk/commit/84cfbcfc488b18a411d50d0c6792da1778703ec8))
- **transaction:** reduce generic constraints related to `ITransaction` ([42dfe82](https://github.com/CareBoo/unity-algorand-sdk/commit/42dfe8207c98c0ddda420ff6be480e0d2c70409c))
- **unitask:** upgrade UniTask to version 2.3.1 ([537d297](https://github.com/CareBoo/unity-algorand-sdk/commit/537d2974dc53c05972ce1ebde197e1ec80580be1))
- **walletconnect:** add `HandshakeUrl` struct ([4238348](https://github.com/CareBoo/unity-algorand-sdk/commit/423834868759538190c6602d1dad1035b2a430a8))
- **walletconnect:** add Algorand WalletConnect client ([#67](https://github.com/CareBoo/unity-algorand-sdk/issues/67)) ([3028858](https://github.com/CareBoo/unity-algorand-sdk/commit/3028858017859fd99a637c05fcd018f1d4c528ea)), closes [#65](https://github.com/CareBoo/unity-algorand-sdk/issues/65)
- **walletconnect:** add QR Code Generator support ([#68](https://github.com/CareBoo/unity-algorand-sdk/issues/68)) ([1f84dcf](https://github.com/CareBoo/unity-algorand-sdk/commit/1f84dcf165fdb6c58989ff0cb2cf2ba1d02f5c3f)), closes [#51](https://github.com/CareBoo/unity-algorand-sdk/issues/51)
- **walletconnect:** add WalletConnect support ([#69](https://github.com/CareBoo/unity-algorand-sdk/issues/69)) ([c650b15](https://github.com/CareBoo/unity-algorand-sdk/commit/c650b15bba0cd97cf592da87c365934ad91bfa6b)), closes [#11](https://github.com/CareBoo/unity-algorand-sdk/issues/11)
- **walletconnect:** add WebSocket support ([e0d2e6d](https://github.com/CareBoo/unity-algorand-sdk/commit/e0d2e6dcdd058ee7544b6f233e02461f87c12ffd)), closes [#59](https://github.com/CareBoo/unity-algorand-sdk/issues/59)

### BREAKING CHANGES

- Json, MessagePack, and Encoding logic has been moved. If you were referencing them
  before you will have to target them in new assemblies now.
- **collections:** All of the extensions to `Unity.Collections` can now be found in
  `Algorand.Unity.Collections` and the `Algorand.Unity.Collections` assembly.
- **websocket:** Namespace `Netcode.Transports.WebSocket` has been renamed to `Algorand.Unity.WebSocket`.
- **assemblies:** `zxing.net` and `websocket-sharp` are now in their own assemblies. You will need to
  update them if you were using the `ZXing` or `WebSocket` namespaces.
- **walletconnect:** Renamed `QrCode` -> `QrCodeUtility`. Set `autoReference = false` in
  `Algorand.Unity.QrCode` assembly. `AlgorandWalletConnectSession.StartConnection` now returns a
  `HandshakeUrl` instead of a `System.String`.
- **walletconnect:** `WalletConnectError` has been removed since it's not being used by the API.
- **qrcode:** QrCode logic has been moved from the `Algorand.Unity.WalletConnect` assembly and
  into the `Algorand.Unity.QrCode` assembly.
- **websocket:** The GUID to the `Algorand.Unity.WebSocket` assembly has changed. You may need to
  re-reference it.
- **lowlevel:** Remove unused struct `NativeReferenceOfDisposable` from Algorand.Unity.LowLevel
- **walletconnect:** Code in the Algorand.Unity.WalletConnect namespace are now in the
  Algorand.Unity.WalletConnect assembly. Additionally, Hex has been moved to the Algorand.Unity namespace
  and remains in the Algorand.Unity assembly.
- **websocket:** To reference Netcode.Transports.WebSocket classes, you need to now reference the
  CareBoo.WebSocket assembly
- **api:** API is no longer returning `UniTask`, and is now returning `AlgoApiRequest.Sent`.

# [2.0.0-pre.11](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.0.0-pre.10...v2.0.0-pre.11) (2022-03-05)

### Code Refactoring

- move json, messagepack, and encoding logic into their own assemblies ([3d76a4b](https://github.com/CareBoo/unity-algorand-sdk/commit/3d76a4b551c0ca671216bb520c79a09482eca410))

### BREAKING CHANGES

- Json, MessagePack, and Encoding logic has been moved. If you were referencing them
  before you will have to target them in new assemblies now.

# [2.0.0-pre.10](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.0.0-pre.9...v2.0.0-pre.10) (2022-03-04)

### Code Refactoring

- **collections:** move `Unity.Collections` extensions into their own assembly ([8d217fc](https://github.com/CareBoo/unity-algorand-sdk/commit/8d217fc9823d427e7b93fff2213a850c84d8ae4d))

### BREAKING CHANGES

- **collections:** All of the extensions to `Unity.Collections` can now be found in
  `Algorand.Unity.Collections` and the `Algorand.Unity.Collections` assembly.

# [2.0.0-pre.9](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.0.0-pre.8...v2.0.0-pre.9) (2022-03-04)

### Code Refactoring

- **assemblies:** move libraries to their own assemblies to simplify things ([0b17ea1](https://github.com/CareBoo/unity-algorand-sdk/commit/0b17ea1758dd53960a82841c4ff3caa130ab2f09))
- **walletconnect:** remove unused enum `WalletConnectError` ([6df5079](https://github.com/CareBoo/unity-algorand-sdk/commit/6df5079fa71d57fbab6dd2bd9f4320758ff349a0))
- **websocket:** change namespace `Netcode.Transports.WebSocket` -> `Algorand.Unity.WebSocket` ([ddb1357](https://github.com/CareBoo/unity-algorand-sdk/commit/ddb1357acf39c14f47b940998bd51268d70bac41))

### Features

- **walletconnect:** add `HandshakeUrl` struct ([4238348](https://github.com/CareBoo/unity-algorand-sdk/commit/423834868759538190c6602d1dad1035b2a430a8))

### BREAKING CHANGES

- **websocket:** Namespace `Netcode.Transports.WebSocket` has been renamed to `Algorand.Unity.WebSocket`.
- **assemblies:** `zxing.net` and `websocket-sharp` are now in their own assemblies. You will need to
  update them if you were using the `ZXing` or `WebSocket` namespaces.
- **walletconnect:** Renamed `QrCode` -> `QrCodeUtility`. Set `autoReference = false` in
  `Algorand.Unity.QrCode` assembly. `AlgorandWalletConnectSession.StartConnection` now returns a
  `HandshakeUrl` instead of a `System.String`.
- **walletconnect:** `WalletConnectError` has been removed since it's not being used by the API.

# [2.0.0-pre.8](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.0.0-pre.7...v2.0.0-pre.8) (2022-03-04)

### Bug Fixes

- **qrcode:** replace zxing dll with its source code ([766c83e](https://github.com/CareBoo/unity-algorand-sdk/commit/766c83eae5ee0530b9bdd50aa7c337d8f5b64eb4)), closes [#84](https://github.com/CareBoo/unity-algorand-sdk/issues/84)
- **websocket:** regenerate guids in `Algorand.Unity.WebSocket` assembly ([3cb3180](https://github.com/CareBoo/unity-algorand-sdk/commit/3cb31803ad00da8239fc077033931badbcea3676)), closes [#83](https://github.com/CareBoo/unity-algorand-sdk/issues/83)

### Features

- **unitask:** upgrade UniTask to version 2.3.1 ([537d297](https://github.com/CareBoo/unity-algorand-sdk/commit/537d2974dc53c05972ce1ebde197e1ec80580be1))

### BREAKING CHANGES

- **qrcode:** QrCode logic has been moved from the `Algorand.Unity.WalletConnect` assembly and
  into the `Algorand.Unity.QrCode` assembly.
- **websocket:** The GUID to the `Algorand.Unity.WebSocket` assembly has changed. You may need to
  re-reference it.

# [2.0.0-pre.7](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.0.0-pre.6...v2.0.0-pre.7) (2022-03-01)

### Features

- **dependencies:** upgrade unity collections to 1.1.0 ([88308be](https://github.com/CareBoo/unity-algorand-sdk/commit/88308be43efed0ab75c8db8942fad4d077fe4b66))

# [2.0.0-pre.6](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.0.0-pre.5...v2.0.0-pre.6) (2022-03-01)

### Bug Fixes

- **walletconnect:** fix compile issue for unity 2020 ([e26836a](https://github.com/CareBoo/unity-algorand-sdk/commit/e26836a3265c04f9908d813080c8c7f915891aeb))

### Features

- **algod:** add overloads for `IAlgodClient.SendTransactions` that allow sending raw msgpack ([4befeeb](https://github.com/CareBoo/unity-algorand-sdk/commit/4befeebdb8f1bc635ed35344eaa8a2d6a762cae6))
- **lowlevel:** add `NativeArrayUtil.ConcatAll` ([10644db](https://github.com/CareBoo/unity-algorand-sdk/commit/10644db4c6e6a5256a3ffd2ea7a1441f0ac4499c))
- **models:** add implicit operators for either value type in an either ([84cfbcf](https://github.com/CareBoo/unity-algorand-sdk/commit/84cfbcfc488b18a411d50d0c6792da1778703ec8))

### BREAKING CHANGES

- **lowlevel:** Remove unused struct `NativeReferenceOfDisposable` from Algorand.Unity.LowLevel

# [2.0.0-pre.5](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.0.0-pre.4...v2.0.0-pre.5) (2022-02-28)

### Features

- **accounts:** add `AccountInfo.EstimateMinBalance` ([51d471b](https://github.com/CareBoo/unity-algorand-sdk/commit/51d471bf2991fd7c406ff3dcbac140b8b8085f38))
- **transaction:** reduce generic constraints related to `ITransaction` ([42dfe82](https://github.com/CareBoo/unity-algorand-sdk/commit/42dfe8207c98c0ddda420ff6be480e0d2c70409c))

# [2.0.0-pre.4](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.0.0-pre.3...v2.0.0-pre.4) (2022-02-28)

### Features

- **api:** add `statusCode` deserialization field to `ErrorResponseFormatter` ([3f69600](https://github.com/CareBoo/unity-algorand-sdk/commit/3f696004496166a1c767e8a3887a3294209ef2b9))

# [2.0.0-pre.3](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.0.0-pre.2...v2.0.0-pre.3) (2022-02-27)

### Bug Fixes

- **walletconnect:** fix issue where new WalletConnect assembly was not compiling in Unity 2020.3 ([0a1ec26](https://github.com/CareBoo/unity-algorand-sdk/commit/0a1ec269d60e6a8d7cec50ea8496b9539b78916b))

### Code Refactoring

- **walletconnect:** move WalletConnect code to its own assembly ([e6668cd](https://github.com/CareBoo/unity-algorand-sdk/commit/e6668cd68b99f81fdd8afedfcae35141e70b2284))

### BREAKING CHANGES

- **walletconnect:** Code in the Algorand.Unity.WalletConnect namespace are now in the
  Algorand.Unity.WalletConnect assembly. Additionally, Hex has been moved to the Algorand.Unity namespace
  and remains in the Algorand.Unity assembly.

# [2.0.0-pre.2](https://github.com/CareBoo/unity-algorand-sdk/compare/v2.0.0-pre.1...v2.0.0-pre.2) (2022-02-25)

### Code Refactoring

- **websocket:** move websocket code to its own assembly definition ([8ff6738](https://github.com/CareBoo/unity-algorand-sdk/commit/8ff6738942b65fe25b27218b80f8cdc630a698d8))

### BREAKING CHANGES

- **websocket:** To reference Netcode.Transports.WebSocket classes, you need to now reference the
  CareBoo.WebSocket assembly

# [2.0.0-pre.1](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.4.0-pre.1...v2.0.0-pre.1) (2022-02-25)

### Features

- **api:** add support for `CancellationToken` and `IProgress<float>` ([6ca2f39](https://github.com/CareBoo/unity-algorand-sdk/commit/6ca2f395c072f97d5f32546feac0b8cec6c64ac9))

### BREAKING CHANGES

- **api:** API is no longer returning `UniTask`, and is now returning `AlgoApiRequest.Sent`.

# [1.4.0-pre.1](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.3.0...v1.4.0-pre.1) (2022-02-24)

### Features

- **walletconnect:** add Algorand WalletConnect client ([#67](https://github.com/CareBoo/unity-algorand-sdk/issues/67)) ([3028858](https://github.com/CareBoo/unity-algorand-sdk/commit/3028858017859fd99a637c05fcd018f1d4c528ea)), closes [#65](https://github.com/CareBoo/unity-algorand-sdk/issues/65)
- **walletconnect:** add QR Code Generator support ([#68](https://github.com/CareBoo/unity-algorand-sdk/issues/68)) ([1f84dcf](https://github.com/CareBoo/unity-algorand-sdk/commit/1f84dcf165fdb6c58989ff0cb2cf2ba1d02f5c3f)), closes [#51](https://github.com/CareBoo/unity-algorand-sdk/issues/51)
- **walletconnect:** add WalletConnect support ([#69](https://github.com/CareBoo/unity-algorand-sdk/issues/69)) ([c650b15](https://github.com/CareBoo/unity-algorand-sdk/commit/c650b15bba0cd97cf592da87c365934ad91bfa6b)), closes [#11](https://github.com/CareBoo/unity-algorand-sdk/issues/11)
- **walletconnect:** add WebSocket support ([e0d2e6d](https://github.com/CareBoo/unity-algorand-sdk/commit/e0d2e6dcdd058ee7544b6f233e02461f87c12ffd)), closes [#59](https://github.com/CareBoo/unity-algorand-sdk/issues/59)

# [1.4.0-exp.4](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.4.0-exp.3...v1.4.0-exp.4) (2022-02-24)

### Features

- **walletconnect:** add WalletConnect support ([#69](https://github.com/CareBoo/unity-algorand-sdk/issues/69)) ([c650b15](https://github.com/CareBoo/unity-algorand-sdk/commit/c650b15bba0cd97cf592da87c365934ad91bfa6b)), closes [#11](https://github.com/CareBoo/unity-algorand-sdk/issues/11)

# [1.4.0-exp.3](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.4.0-exp.2...v1.4.0-exp.3) (2022-02-22)

### Features

- **walletconnect:** add QR Code Generator support ([#68](https://github.com/CareBoo/unity-algorand-sdk/issues/68)) ([1f84dcf](https://github.com/CareBoo/unity-algorand-sdk/commit/1f84dcf165fdb6c58989ff0cb2cf2ba1d02f5c3f)), closes [#51](https://github.com/CareBoo/unity-algorand-sdk/issues/51)

# [1.4.0-exp.2](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.4.0-exp.1...v1.4.0-exp.2) (2022-02-21)

### Features

- **walletconnect:** add Algorand WalletConnect client ([#67](https://github.com/CareBoo/unity-algorand-sdk/issues/67)) ([3028858](https://github.com/CareBoo/unity-algorand-sdk/commit/3028858017859fd99a637c05fcd018f1d4c528ea)), closes [#65](https://github.com/CareBoo/unity-algorand-sdk/issues/65)

# [1.4.0-exp.1](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.3.0...v1.4.0-exp.1) (2022-02-18)

### Features

- **walletconnect:** add WebSocket support ([e0d2e6d](https://github.com/CareBoo/unity-algorand-sdk/commit/e0d2e6dcdd058ee7544b6f233e02461f87c12ffd)), closes [#59](https://github.com/CareBoo/unity-algorand-sdk/issues/59)

# [1.3.0](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.2.2...v1.3.0) (2021-11-19)

### Features

- **logic:** add `Logic.Sign` for signing programs with private keys ([131ef28](https://github.com/CareBoo/unity-algorand-sdk/commit/131ef285fbb5c18bff3850ebceb7ae8420d2dc69))
- **logic:** add `LogicSig.GetAddress` ([9628a9a](https://github.com/CareBoo/unity-algorand-sdk/commit/9628a9ac857254d69f42bc4ebca500d2098865d0))
- **multisig:** add `Multisig.GetAddress` ([e490459](https://github.com/CareBoo/unity-algorand-sdk/commit/e4904598e21caef8bac504b8b0f1917a1b465b9a))

## [1.2.2](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.2.1...v1.2.2) (2021-11-15)

### Bug Fixes

- **algoapiobject:** fix compile err ([12ca53c](https://github.com/CareBoo/unity-algorand-sdk/commit/12ca53cedde5408961eabf7d1790b39dd2cfc572))
- **assetparams:** change size of URL FixedString to 128 bytes ([792d207](https://github.com/CareBoo/unity-algorand-sdk/commit/792d207e4d4d082e076f57c62ab51876c7c3ceec))

## [1.2.2-pre.2](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.2.2-pre.1...v1.2.2-pre.2) (2021-11-15)

### Bug Fixes

- **algoapiobject:** fix compile err ([13692b8](https://github.com/CareBoo/unity-algorand-sdk/commit/13692b812ef959e501dcc27607e41e9e67b41d7e))

## [1.2.2-pre.1](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.2.1...v1.2.2-pre.1) (2021-11-13)

### Bug Fixes

- **assetparams:** change size of URL FixedString to 128 bytes ([c964899](https://github.com/CareBoo/unity-algorand-sdk/commit/c96489905b62b5c3ecef1aeabd3e56fa4f15dc28))

## [1.2.1](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.2.0...v1.2.1) (2021-11-09)

### Bug Fixes

- **error response:** fix some responses with response code 0 incorrectly treated as errors ([e605e75](https://github.com/CareBoo/unity-algorand-sdk/commit/e605e75ee0fef521acc9c73ee311812b9eda3fc2))

# [1.2.0](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.1.0...v1.2.0) (2021-11-08)

### Bug Fixes

- **json:** fix MultiSig JSON format using incorrect field names ([83d0950](https://github.com/CareBoo/unity-algorand-sdk/commit/83d09509d990f6fdbc299733a3d2deea6e89d2c7))
- **logging:** remove unnecessary debug logs ([7550061](https://github.com/CareBoo/unity-algorand-sdk/commit/755006192bec926fff6b77249961de592845aaa1))
- **msgpack:** fix enum serialization when using IL2CPP ([0c1a4d0](https://github.com/CareBoo/unity-algorand-sdk/commit/0c1a4d0a128baed173a60db3d1b9ee788c836e6b))
- **plugins:** remove "lib" prefix from certain plugins ([0ecf98f](https://github.com/CareBoo/unity-algorand-sdk/commit/0ecf98f052b0f6a9c2fb1b376fd29d7e4578112e)), closes [#55](https://github.com/CareBoo/unity-algorand-sdk/issues/55)
- **plugins:** remove unnecessary android static library plugin ([fc09b66](https://github.com/CareBoo/unity-algorand-sdk/commit/fc09b668dc4b58896678ccf51e70ed10f8f95383))
- **teal:** fix `TealValue` deserialization failing when both uint and bytes values included ([22cfcd0](https://github.com/CareBoo/unity-algorand-sdk/commit/22cfcd08cfef6ba25404a6123edca3d2aff9d6fe))
- **web response:** fix `ErrorResponse.IsError` not returning `true` when `responseCode == 0` ([3e1c8d6](https://github.com/CareBoo/unity-algorand-sdk/commit/3e1c8d6aefb868683ef1dc241173674990081c70)), closes [#58](https://github.com/CareBoo/unity-algorand-sdk/issues/58)

### Features

- **api clients:** add constructor to api clients that makes the token parameter optional ([0bcd681](https://github.com/CareBoo/unity-algorand-sdk/commit/0bcd681ebb7f92822342a7bea5a9b448c7b03ec6))
- **api:** add custom Request HTTP header support ([a6f51f8](https://github.com/CareBoo/unity-algorand-sdk/commit/a6f51f8df7bd2f61b3fcf02cbc61932d849d80aa)), closes [#58](https://github.com/CareBoo/unity-algorand-sdk/issues/58)

### Performance Improvements

- **plugins:** replace debug mode libsodium libraries with release mode ([d7638c8](https://github.com/CareBoo/unity-algorand-sdk/commit/d7638c8fbe36dfd0a07bca01df8c0b46131b30af)), closes [#55](https://github.com/CareBoo/unity-algorand-sdk/issues/55)

# [1.2.0-pre.3](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.2.0-pre.2...v1.2.0-pre.3) (2021-11-08)

### Bug Fixes

- **web response:** fix `ErrorResponse.IsError` not returning `true` when `responseCode == 0` ([3e1c8d6](https://github.com/CareBoo/unity-algorand-sdk/commit/3e1c8d6aefb868683ef1dc241173674990081c70)), closes [#58](https://github.com/CareBoo/unity-algorand-sdk/issues/58)

# [1.2.0-pre.2](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.2.0-pre.1...v1.2.0-pre.2) (2021-11-07)

### Features

- **api clients:** add constructor to api clients that makes the token parameter optional ([0bcd681](https://github.com/CareBoo/unity-algorand-sdk/commit/0bcd681ebb7f92822342a7bea5a9b448c7b03ec6))

# [1.2.0-pre.1](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.1.1-pre.1...v1.2.0-pre.1) (2021-11-07)

### Bug Fixes

- **json:** fix MultiSig JSON format using incorrect field names ([83d0950](https://github.com/CareBoo/unity-algorand-sdk/commit/83d09509d990f6fdbc299733a3d2deea6e89d2c7))
- **teal:** fix `TealValue` deserialization failing when both uint and bytes values included ([22cfcd0](https://github.com/CareBoo/unity-algorand-sdk/commit/22cfcd08cfef6ba25404a6123edca3d2aff9d6fe))

### Features

- **api:** add custom Request HTTP header support ([a6f51f8](https://github.com/CareBoo/unity-algorand-sdk/commit/a6f51f8df7bd2f61b3fcf02cbc61932d849d80aa)), closes [#58](https://github.com/CareBoo/unity-algorand-sdk/issues/58)

### Performance Improvements

- **plugins:** replace debug mode libsodium libraries with release mode ([d7638c8](https://github.com/CareBoo/unity-algorand-sdk/commit/d7638c8fbe36dfd0a07bca01df8c0b46131b30af)), closes [#55](https://github.com/CareBoo/unity-algorand-sdk/issues/55)

## [1.1.1-pre.1](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.1.0...v1.1.1-pre.1) (2021-11-05)

### Bug Fixes

- **logging:** remove unnecessary debug logs ([7550061](https://github.com/CareBoo/unity-algorand-sdk/commit/755006192bec926fff6b77249961de592845aaa1))
- **msgpack:** fix enum serialization when using IL2CPP ([0c1a4d0](https://github.com/CareBoo/unity-algorand-sdk/commit/0c1a4d0a128baed173a60db3d1b9ee788c836e6b))
- **plugins:** remove "lib" prefix from certain plugins ([0ecf98f](https://github.com/CareBoo/unity-algorand-sdk/commit/0ecf98f052b0f6a9c2fb1b376fd29d7e4578112e)), closes [#55](https://github.com/CareBoo/unity-algorand-sdk/issues/55)
- **plugins:** remove unnecessary android static library plugin ([fc09b66](https://github.com/CareBoo/unity-algorand-sdk/commit/fc09b668dc4b58896678ccf51e70ed10f8f95383))

# [1.1.0](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0...v1.1.0) (2021-11-03)

### Bug Fixes

- add missing meta files ([2407941](https://github.com/CareBoo/unity-algorand-sdk/commit/24079419c06ea7c5608d601219239322f7d4b5ab))
- **osx:** fix multiple sodium.bundle plugins ([b3b9389](https://github.com/CareBoo/unity-algorand-sdk/commit/b3b93890b5ec0bde98bbf5d35fd4246475b6cc0a))

### Features

- **editor:** Make many models serializable and add property drawers to render them in editor ([09185fc](https://github.com/CareBoo/unity-algorand-sdk/commit/09185fce14850cb2d4f9a42be98f077e090608ac)), closes [#41](https://github.com/CareBoo/unity-algorand-sdk/issues/41)
- support WebGL ([1890694](https://github.com/CareBoo/unity-algorand-sdk/commit/18906948adbb7c5e42dd3988a738dccd14b4cd3d)), closes [#52](https://github.com/CareBoo/unity-algorand-sdk/issues/52)

# [1.1.0-pre.3](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.1.0-pre.2...v1.1.0-pre.3) (2021-11-03)

### Features

- **editor:** Make many models serializable and add property drawers to render them in editor ([09185fc](https://github.com/CareBoo/unity-algorand-sdk/commit/09185fce14850cb2d4f9a42be98f077e090608ac)), closes [#41](https://github.com/CareBoo/unity-algorand-sdk/issues/41)

# [1.1.0-pre.2](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.1.0-pre.1...v1.1.0-pre.2) (2021-11-01)

### Bug Fixes

- **osx:** fix multiple sodium.bundle plugins ([b3b9389](https://github.com/CareBoo/unity-algorand-sdk/commit/b3b93890b5ec0bde98bbf5d35fd4246475b6cc0a))

# [1.1.0-pre.1](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.1-pre.1...v1.1.0-pre.1) (2021-10-31)

### Features

- support WebGL ([1890694](https://github.com/CareBoo/unity-algorand-sdk/commit/18906948adbb7c5e42dd3988a738dccd14b4cd3d)), closes [#52](https://github.com/CareBoo/unity-algorand-sdk/issues/52)

## [1.0.1-pre.1](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0...v1.0.1-pre.1) (2021-10-29)

### Bug Fixes

- add missing meta files ([2407941](https://github.com/CareBoo/unity-algorand-sdk/commit/24079419c06ea7c5608d601219239322f7d4b5ab))

# 1.0.0 (2021-10-29)

### Bug Fixes

- :bug: fix `RawTransaction.Equals` ([b0769da](https://github.com/CareBoo/unity-algorand-sdk/commit/b0769da141c3fd40d90368a83467ea6de5167af1))
- :bug: fix compile errs ([9e59233](https://github.com/CareBoo/unity-algorand-sdk/commit/9e59233acf1419c2543d0a9b9c0ecec9c2abdd74))
- :bug: fix crash on ArrayComparer.Equals ([e2931e0](https://github.com/CareBoo/unity-algorand-sdk/commit/e2931e0f717bb18118b4518302426f9474a6dfee))
- :bug: fix issues with codegen and AOT compilation ([4d4b583](https://github.com/CareBoo/unity-algorand-sdk/commit/4d4b583f0d43b456569d54a39940b0c435b6c96c))
- :bug: fix keyworded enum types formatting as numbers ([ee6f363](https://github.com/CareBoo/unity-algorand-sdk/commit/ee6f36332d9f6ec73c94a2ef21d2400ee4cde657))
- :bug: Fix npm release ([e3fed38](https://github.com/CareBoo/unity-algorand-sdk/commit/e3fed383886b464ed1f7dd400787c2d4c3abd272))
- :bug: Fix repository ([b4efa6e](https://github.com/CareBoo/unity-algorand-sdk/commit/b4efa6e02aab21efad9cd2e51aa06abdf83757c0))
- :bug: fix signatures ([1e3a9cc](https://github.com/CareBoo/unity-algorand-sdk/commit/1e3a9cc801a282e46d0d4416489a4c53dd134161))
- :triangular_flag_on_post: Update package registry ([64987f8](https://github.com/CareBoo/unity-algorand-sdk/commit/64987f8c68b83d5abcc9a8a10bbac3c60e934117))
- :white_check_mark: Fix CI tests on Github Actions can't find libsodium ([db8718a](https://github.com/CareBoo/unity-algorand-sdk/commit/db8718aa2de3a218789d513b40f7b19345771690)), closes [#12](https://github.com/CareBoo/unity-algorand-sdk/issues/12)
- **algod:** :bug: fix `AlgodClient.TealCompile` ([bd05ed3](https://github.com/CareBoo/unity-algorand-sdk/commit/bd05ed30d3f283fc13184b5ae65be486b709b7fa)), closes [#20](https://github.com/CareBoo/unity-algorand-sdk/issues/20)
- **algod:** :bug: fix `AssetParams` serialization ([6f2b830](https://github.com/CareBoo/unity-algorand-sdk/commit/6f2b83070ecc4c21457732dfbfb9d708a641502b))
- **algod:** :bug: fix pending transactions not returning messagepack ([3b881a3](https://github.com/CareBoo/unity-algorand-sdk/commit/3b881a3b6290fadd22046ad85d49dc26f733b7ef))
- **algod:** :bug: fix vrfpubkey formatter lookup ([08a09ae](https://github.com/CareBoo/unity-algorand-sdk/commit/08a09ae8908ecce9695d8527d3f102e0fb487479))
- **algod:** :bug: fix vrfpubkey formatting ([f3c1a56](https://github.com/CareBoo/unity-algorand-sdk/commit/f3c1a563f3e01f8cb27a79b16c7bc395556c242d))
- **algod:** :bug: replace `VrfPubKey` with `FixedString128Bytes` ([e6ce383](https://github.com/CareBoo/unity-algorand-sdk/commit/e6ce3832920e3d6a2add10b731cabf507e69b21f)), closes [#42](https://github.com/CareBoo/unity-algorand-sdk/issues/42)
- **crypto:** fix libsodium not working on OSX ([cb0b2c4](https://github.com/CareBoo/unity-algorand-sdk/commit/cb0b2c456eef7e3a0c3bd6664cfd3aaad3e0826d))
- fix empty dirs finding their way into the project ([5a00890](https://github.com/CareBoo/unity-algorand-sdk/commit/5a00890475ced1d585d0f137d9f7d577a6f862ef))
- **indexer:** :bug: add missing fields to `Account` model ([86122de](https://github.com/CareBoo/unity-algorand-sdk/commit/86122deb97cc6b99074415c1c069f36e978fe4c7))
- **indexer:** :bug: add token back to indexer ([c704eb2](https://github.com/CareBoo/unity-algorand-sdk/commit/c704eb2196a0f2764c1c24982fcec4448fd14142))
- **indexer:** :bug: fix `HealthCheck` missing serialization logic ([f11aa38](https://github.com/CareBoo/unity-algorand-sdk/commit/f11aa384567a2b04a5eadebe8135a9cce8f77df5))
- **indexer:** :bug: fix indexer requires token ([ef2d461](https://github.com/CareBoo/unity-algorand-sdk/commit/ef2d461037a2e1c6805cb07456a4b501f8ea502b))
- **indexer:** :bug: fix transaction formatters missing valid msgpack fields ([e18f84a](https://github.com/CareBoo/unity-algorand-sdk/commit/e18f84a5b54a011aa353d8cc47cc06d9129e3a3d))
- **indexer:** :fire: fix huge issue with indexer where query parameters were in body ([92cb794](https://github.com/CareBoo/unity-algorand-sdk/commit/92cb7946a88c0c4169eec9d4673f9a102065cfa1))
- **indexer:** :sparkles: fix `TealValue` msgpack fieldnames ([2da8001](https://github.com/CareBoo/unity-algorand-sdk/commit/2da8001d1963ebea0f5541a0cba43d8d21177790))
- **indexer:** fix missing `Version` field in `HealthCheck` ([f70c73d](https://github.com/CareBoo/unity-algorand-sdk/commit/f70c73d5d5dd6d07442a63f3e07d4c2280fcd08c))
- **json:** :bug: fix `PrivateKey` JSON deserialization err ([63baf88](https://github.com/CareBoo/unity-algorand-sdk/commit/63baf885f7a9d2efb26a7cd11da3392e255cdb59))
- **json:** :bug: fix empty json objects missing begin object '{' char ([d2b0539](https://github.com/CareBoo/unity-algorand-sdk/commit/d2b0539bc7ea9ef5e2a9527776ce959995cfb35e))
- **kmd:** :art: fix `KmdClient` API incorrectly using optional args ([29374e1](https://github.com/CareBoo/unity-algorand-sdk/commit/29374e10748775c1b3416b6918a0ca8ba581ba6e)), closes [#36](https://github.com/CareBoo/unity-algorand-sdk/issues/36)
- **kmd:** :bug: fix `GenerateKeyRequest` to use `wallet_handle_token` not password ([19c3215](https://github.com/CareBoo/unity-algorand-sdk/commit/19c321578986a83807e080eb4f8bbc66aa139490))
- **kmd:** :bug: fix `ImportKeyRequest` to use wallet_handle_token not password ([4a60a6c](https://github.com/CareBoo/unity-algorand-sdk/commit/4a60a6cf242a148bc00cf4db885e1cade00050ed))
- **kmd:** :bug: fix `KmdClient.SignTransaction` returning a signed transaction message ([6df57ec](https://github.com/CareBoo/unity-algorand-sdk/commit/6df57ece5c3da0ae2f9319680b0c879281bd1de6))
- **kmd:** :bug: fix `WalletHandle` not added to formatter cache ([be0495d](https://github.com/CareBoo/unity-algorand-sdk/commit/be0495d641426a54d59c87a430906b38047b01ba))
- **kmd:** :bug: fix multisig ([78559d0](https://github.com/CareBoo/unity-algorand-sdk/commit/78559d0c62326a8a7b6d7d0d45c39813428830f5))
- **serialization:** :bug: fix `Block` not having a formatter ([d840dfe](https://github.com/CareBoo/unity-algorand-sdk/commit/d840dfef2aeaefd9e531ca10f19ab62fb41ddeec))
- **serialization:** :bug: fix `BlockResponse` Serialization ([6925142](https://github.com/CareBoo/unity-algorand-sdk/commit/69251420e5b3eb2c1c8e32c07ebe1bd38c0433ef))
- **serialization:** :bug: fix `JsonWriter` not writing to `NativeText` ([9a21502](https://github.com/CareBoo/unity-algorand-sdk/commit/9a215021943f26e44aa89f867c881ab6a5bf4e79))
- **serialization:** :bug: fix `SignedTransaction` serialization ([2f7bc6b](https://github.com/CareBoo/unity-algorand-sdk/commit/2f7bc6b566d912efdef57a7fb6c8e8cfb1450cbd))
- **serialization:** :bug: fix `TransactionId` serialization ([f4fcb74](https://github.com/CareBoo/unity-algorand-sdk/commit/f4fcb7406bdd74d1ec3b36bca930afc5772e622b))
- **serialization:** :bug: fix discrepancy between `TransactionId` and `TransactinIdResponse` ([4bef93f](https://github.com/CareBoo/unity-algorand-sdk/commit/4bef93f912c2026852e9d523ade6ca7817c75950))
- **serialization:** :bug: fix issue where fixed strings were incorrectly being added to the queryparams ([338790b](https://github.com/CareBoo/unity-algorand-sdk/commit/338790b5a315297ae670b5e38a232661751a13f2))
- **serialization:** :bug: fix missing `AlgoApiObject` on all Transaction Params ([d8f5b27](https://github.com/CareBoo/unity-algorand-sdk/commit/d8f5b27040cdbd30700bffd0cda21f8e48ba5dd2))
- **transaction:** :bug: fix `OnCompletion` having incorrect byte values ([9786105](https://github.com/CareBoo/unity-algorand-sdk/commit/9786105f1cfe089c7329d3c157d9df91e601b69c))
- **transaction:** :bug: fix incorrect constructor in `Transaction.ApplicationCall` ([521fb98](https://github.com/CareBoo/unity-algorand-sdk/commit/521fb98be04ca801296a17b4091e4b64caa48d05))
- **transaction:** remove unused assetCloseTo param in `AssetClawback` ([ce4d004](https://github.com/CareBoo/unity-algorand-sdk/commit/ce4d004a09f32530870a28d1652bc3747fa3e928))

### Code Refactoring

- :fire: remove `NativeSliceExtensions` ([325bfa9](https://github.com/CareBoo/unity-algorand-sdk/commit/325bfa9c03ca342bc939f7862c6beebe1796e678))
- :fire: remove some verify methods in the transaction ([2432bdd](https://github.com/CareBoo/unity-algorand-sdk/commit/2432bdd1ec1a4a105bea297b413a80921a9d70ee))
- :fire: remove unnecessary `SendTransactionRaw` ([552dac8](https://github.com/CareBoo/unity-algorand-sdk/commit/552dac85bf6b4bff29d3a05c98b9a0ecc66e230c))
- :recycle: convert all `SignedTransaction<>` to a single `SignedTransaction` ([4909d60](https://github.com/CareBoo/unity-algorand-sdk/commit/4909d6056a2291f9aa21a757ed62a70f3568ed61))
- :recycle: rename `RawSignedTransaction` -> `SignedTransaction` ([787b160](https://github.com/CareBoo/unity-algorand-sdk/commit/787b160468d159bd0cac67abc74e7ed9e81ae8ed))
- :recycle: rename `RawTransaction` -> `Transaction` ([b3c4129](https://github.com/CareBoo/unity-algorand-sdk/commit/b3c412904be573b2bcff9cbc50a0616595ce2dea))
- :recycle: rename AlgoApiKeyAttribute -> AlgoApiFieldAttribute ([607dab2](https://github.com/CareBoo/unity-algorand-sdk/commit/607dab20f38e791239e7aaf240c9752ed6b6df94))
- **algod:** :recycle: rename `Block` -> `BlockResponse` ([de1999c](https://github.com/CareBoo/unity-algorand-sdk/commit/de1999c2d9860d7394ce692b33f5decfc9522756))
- **algod:** :recycle: replace `AccountParticipation` with `Transaction.KeyRegistration.Params` ([b7e9ba7](https://github.com/CareBoo/unity-algorand-sdk/commit/b7e9ba73da80e1ecd1bc6c5ea9b0440111b49ed1))
- **kmd:** :recycle: replace `Request` with explicit method params ([1b63b9b](https://github.com/CareBoo/unity-algorand-sdk/commit/1b63b9be42288ccb4b57d02c6c945a567e8a9eb7))
- remove .NET 4.8 requirement ([077ea1e](https://github.com/CareBoo/unity-algorand-sdk/commit/077ea1e62db9ee057a4d782f7750ffe4af9e9cbd)), closes [#19](https://github.com/CareBoo/unity-algorand-sdk/issues/19) [#9](https://github.com/CareBoo/unity-algorand-sdk/issues/9)

### Features

- :construction_worker: Add npmjs support ([42c57c3](https://github.com/CareBoo/unity-algorand-sdk/commit/42c57c389081fd2b9c25b797245f899d989f9a95))
- :heavy_plus_sign: Add libsodium ios, android, and windows ([066a8ad](https://github.com/CareBoo/unity-algorand-sdk/commit/066a8ad97732d3358ed9de8282c298015c95ccf6))
- :sparkles: Add Account.Generate ([8cfea18](https://github.com/CareBoo/unity-algorand-sdk/commit/8cfea181e464adff165db56a94ad3eb83290f571))
- :sparkles: Add Address struct ([9c63147](https://github.com/CareBoo/unity-algorand-sdk/commit/9c6314716058530bdd79984f7ea3e016b2283928))
- :sparkles: Add basic signed transaction support ([64bfae2](https://github.com/CareBoo/unity-algorand-sdk/commit/64bfae28348d3aff964e2de8f88517ce713827e3))
- :sparkles: Add basic transaction support for message pack serialization and deserialization ([13dc164](https://github.com/CareBoo/unity-algorand-sdk/commit/13dc16404fdc433c75108b181d98f72d3e9d62ab))
- :sparkles: Add checksums ([1427b4c](https://github.com/CareBoo/unity-algorand-sdk/commit/1427b4c19d3379f3d165db1c72c468a55b1ca35b))
- :sparkles: add FixedBytesFormatter ([e686c0d](https://github.com/CareBoo/unity-algorand-sdk/commit/e686c0dacab26cf31704defb3e8fdf062f5f4d47))
- :sparkles: Add IEquatable, GetHashCode for RawTransaction ([fea1031](https://github.com/CareBoo/unity-algorand-sdk/commit/fea1031ff5e2f5fb69a16e1257c99c2cbe04f12d))
- :sparkles: add LogicSig implementation ([bfcea16](https://github.com/CareBoo/unity-algorand-sdk/commit/bfcea16c6c07f3e01b68f36dc52486405ee9fceb))
- :sparkles: Add Mnemonic.FromString and Mnemonic.ToString ([5f2e0ef](https://github.com/CareBoo/unity-algorand-sdk/commit/5f2e0ef99bf17599795d9fbf74f891a4cb671db4))
- :sparkles: add readOnly support ([4f94ee4](https://github.com/CareBoo/unity-algorand-sdk/commit/4f94ee487d99435bf794fa390c2828679c28c486))
- :sparkles: Add Sha512 interop ([72412b8](https://github.com/CareBoo/unity-algorand-sdk/commit/72412b88e1cf1e0f9199b2bccbe139bfcdfd0850))
- :sparkles: Add start of Native Integration with Libsodium ([04dadfb](https://github.com/CareBoo/unity-algorand-sdk/commit/04dadfbbe27cee981f3338d59578315bb93c940c))
- :sparkles: Added Transaction Header, Payment ([f4f5004](https://github.com/CareBoo/unity-algorand-sdk/commit/f4f5004193dac2a5b39b9861de345922bea69479))
- :sparkles: implement asset configuration transaction ([15201f2](https://github.com/CareBoo/unity-algorand-sdk/commit/15201f272896f68729c6fdddcfdb3de6ab685c3e)), closes [#27](https://github.com/CareBoo/unity-algorand-sdk/issues/27)
- :sparkles: implement AssetFreezeTransaction ([aeb6bf7](https://github.com/CareBoo/unity-algorand-sdk/commit/aeb6bf7b587185cc5cf083d800a66d71940faeba)), closes [#26](https://github.com/CareBoo/unity-algorand-sdk/issues/26)
- :sparkles: implement AssetTransfer Transaction ([2d74093](https://github.com/CareBoo/unity-algorand-sdk/commit/2d740930d91df326725d9ac4a6766945f8f17135)), closes [#23](https://github.com/CareBoo/unity-algorand-sdk/issues/23)
- :sparkles: update AlgoApiFormatterLookup ([509f021](https://github.com/CareBoo/unity-algorand-sdk/commit/509f02187fe81bca24e68db8ebdaf1a203c6d3da))
- Add Mnemonic and Key datastructs ([0338af0](https://github.com/CareBoo/unity-algorand-sdk/commit/0338af06b891e1abea5ecff2fb7cda4e0d276489))
- Add signed transaction support for payment transactions ([4cb4b4c](https://github.com/CareBoo/unity-algorand-sdk/commit/4cb4b4cd0ec4c79f98b27aa787d96548ba007d75))
- **algod:** add support for sending a group of transactions ([c2337e0](https://github.com/CareBoo/unity-algorand-sdk/commit/c2337e0a9c0f45cb31ffa08ae7cfe159651eaded))
- **algod:** implement Algod Client ([ba2920e](https://github.com/CareBoo/unity-algorand-sdk/commit/ba2920e9ca39656cc395650e90416a6ae2b5fee3)), closes [#10](https://github.com/CareBoo/unity-algorand-sdk/issues/10) [#16](https://github.com/CareBoo/unity-algorand-sdk/issues/16) [#18](https://github.com/CareBoo/unity-algorand-sdk/issues/18)
- **indexer:** :recycle: rename `ApplicationStateSchema` -> `StateSchema` ([c2c1752](https://github.com/CareBoo/unity-algorand-sdk/commit/c2c17527209d18f05646173d4ad6f5a9931584bc))
- **indexer:** :sparkles: add `IIndexerClient` ([c383659](https://github.com/CareBoo/unity-algorand-sdk/commit/c383659059b43efe5d2f95c113f0285147e1b199))
- **indexer:** :sparkles: implement `AccountQuery` ([3bc4c45](https://github.com/CareBoo/unity-algorand-sdk/commit/3bc4c45221247b7199e8a3777cf7370fee6ca996))
- **indexer:** :sparkles: implement `AccountResponse` ([df7875c](https://github.com/CareBoo/unity-algorand-sdk/commit/df7875c2194ae125c1fd98da95d7ae51a3659383))
- **indexer:** :sparkles: implement `AccountsQuery` ([eb4f138](https://github.com/CareBoo/unity-algorand-sdk/commit/eb4f1383fe3ab49930ee23f7f4872c4f5ec98d89))
- **indexer:** :sparkles: implement `AccountsResponse` ([15b95a0](https://github.com/CareBoo/unity-algorand-sdk/commit/15b95a0d5a934c091c710f9f85da6d9f7892f269))
- **indexer:** :sparkles: implement `AddressRole` ([819fee1](https://github.com/CareBoo/unity-algorand-sdk/commit/819fee17d8cae4a9719c3ec5215a1163eac0e8b6))
- **indexer:** :sparkles: implement `Application` ([6efc045](https://github.com/CareBoo/unity-algorand-sdk/commit/6efc045b7290327aea91c7d13e7552a1b2a66de3))
- **indexer:** :sparkles: implement `ApplicationLocalState` ([b9f5e19](https://github.com/CareBoo/unity-algorand-sdk/commit/b9f5e19c483316ef48234f41453e29b753d04993))
- **indexer:** :sparkles: implement `ApplicationQuery` ([90529fe](https://github.com/CareBoo/unity-algorand-sdk/commit/90529feb9962a64e31ff13ace2926c4009cfb906))
- **indexer:** :sparkles: implement `ApplicationResponse` ([2968a38](https://github.com/CareBoo/unity-algorand-sdk/commit/2968a3886f1be540e660c3b0b5a9aad389b6f1c0))
- **indexer:** :sparkles: implement `ApplicationsQuery` ([1b2a010](https://github.com/CareBoo/unity-algorand-sdk/commit/1b2a0100f02c8b51c53be54bcafe069fe92e5658))
- **indexer:** :sparkles: implement `ApplicationsResponse` ([f3120b3](https://github.com/CareBoo/unity-algorand-sdk/commit/f3120b39e60549a5d0b23a0730cc8433fd65f297))
- **indexer:** :sparkles: implement `Asset` ([3e2b9e6](https://github.com/CareBoo/unity-algorand-sdk/commit/3e2b9e6d602cf26ed7535927ff30f9d777913fef))
- **indexer:** :sparkles: implement `AssetHolding` ([b7e6714](https://github.com/CareBoo/unity-algorand-sdk/commit/b7e67142a06c504e5f2a97974550cc810bbdb9ba))
- **indexer:** :sparkles: implement `AssetParams` ([3e25148](https://github.com/CareBoo/unity-algorand-sdk/commit/3e25148ef21ade54167f71d1fd8c02fcbd4f476a))
- **indexer:** :sparkles: implement `AssetQuery` ([2c8f30d](https://github.com/CareBoo/unity-algorand-sdk/commit/2c8f30d79e0847101b898b983891addf595dd37c))
- **indexer:** :sparkles: implement `AssetResponse` ([f44da47](https://github.com/CareBoo/unity-algorand-sdk/commit/f44da4723ef5dbcd93049f1cdb4ec759ff2ca530))
- **indexer:** :sparkles: implement `AssetsResponse` ([abf6ccd](https://github.com/CareBoo/unity-algorand-sdk/commit/abf6ccd0ba15fd30503fe5edb875ac8e66949e49))
- **indexer:** :sparkles: implement `BalancesQuery` ([37c652e](https://github.com/CareBoo/unity-algorand-sdk/commit/37c652e458fde70485ec1d9f75a6f044e3459848))
- **indexer:** :sparkles: implement `BalancesResponse` ([bef374a](https://github.com/CareBoo/unity-algorand-sdk/commit/bef374aa161f29885984f12f117d51586af36f98))
- **indexer:** :sparkles: implement `Block` ([8168949](https://github.com/CareBoo/unity-algorand-sdk/commit/8168949039df1448008280485699282e5a3ae142))
- **indexer:** :sparkles: implement `BlockRewards` ([43a2e0d](https://github.com/CareBoo/unity-algorand-sdk/commit/43a2e0de5f124cf73bda585efafacef4cb75fdf3))
- **indexer:** :sparkles: implement `BlockUpgradeStatus` ([34ee633](https://github.com/CareBoo/unity-algorand-sdk/commit/34ee633991fff8fd67c219d1bc07dd4f6e064a28))
- **indexer:** :sparkles: implement `BlockUpgradeVote` ([46bfbdf](https://github.com/CareBoo/unity-algorand-sdk/commit/46bfbdfb01f23b30804027c4e38d4f64960353a7))
- **indexer:** :sparkles: implement `ErrorResponse` msgpack fields ([7bb31e4](https://github.com/CareBoo/unity-algorand-sdk/commit/7bb31e43d0cae3c0ff28e21d579ef6a3d18012e5))
- **indexer:** :sparkles: implement `EvalDelta` msgpack fields ([e7ce847](https://github.com/CareBoo/unity-algorand-sdk/commit/e7ce847312a539ff9ef5ba8d24b2373270825de5))
- **indexer:** :sparkles: implement `EvalDeltaKeyValue` msgpack fields ([f564579](https://github.com/CareBoo/unity-algorand-sdk/commit/f564579c8b488b94926d6670b1c50e82525f7ff3))
- **indexer:** :sparkles: implement `HealthCheck` ([57e38ee](https://github.com/CareBoo/unity-algorand-sdk/commit/57e38ee9f0a4a56c50752dab0305bd0becbb775e))
- **indexer:** :sparkles: implement `IndexerClient.GetAccount` ([e088877](https://github.com/CareBoo/unity-algorand-sdk/commit/e08887724a5a393dfcf88a6fe04e8b98e780a264))
- **indexer:** :sparkles: implement `IndexerClient.GetAccounts` ([1fa9f5b](https://github.com/CareBoo/unity-algorand-sdk/commit/1fa9f5b906f9ae31289043faadacc510affa582a))
- **indexer:** :sparkles: implement `IndexerClient.GetHealth` ([ee98909](https://github.com/CareBoo/unity-algorand-sdk/commit/ee989091c0a1dc74e491b337ed2cb486aee66b3b))
- **indexer:** :sparkles: implement `IndexerClient` ([b39c115](https://github.com/CareBoo/unity-algorand-sdk/commit/b39c1157689abcdbd1e19ad14363bd6d63d454a7))
- **indexer:** :sparkles: implement `LogicSig` json fields ([a547772](https://github.com/CareBoo/unity-algorand-sdk/commit/a5477728821094296cf2b1053223f49b8a34fd8f))
- **indexer:** :sparkles: implement `MiniAssetHolding` ([6df0bd0](https://github.com/CareBoo/unity-algorand-sdk/commit/6df0bd0048ea48ae70aa63d19e851910f4d8c4b9))
- **indexer:** :sparkles: implement `MultiSig.SubSignature` ([d995b21](https://github.com/CareBoo/unity-algorand-sdk/commit/d995b211ad9d1ded2159ac8700864513f375456b))
- **indexer:** :sparkles: implement `MultiSig` ([2b33c02](https://github.com/CareBoo/unity-algorand-sdk/commit/2b33c0263329b4d675b5ef5dc2e72995ea30e3d0))
- **indexer:** :sparkles: implement `OnCompletion` ([b7181f3](https://github.com/CareBoo/unity-algorand-sdk/commit/b7181f318265347807e74b8b34a84a084ed4c586))
- **indexer:** :sparkles: implement `TealKeyValue` msgPack fields ([bfcb1ad](https://github.com/CareBoo/unity-algorand-sdk/commit/bfcb1ade5bb620630a80f50dc8b6b0d147b4435a))
- **indexer:** :sparkles: implement `Transaction.ApplicationCall` ([4e800be](https://github.com/CareBoo/unity-algorand-sdk/commit/4e800be0301f6df03b67ef3d98c686becb33b296))
- **indexer:** :sparkles: implement `Transaction.ApplicationCall` model fields ([e05a1b3](https://github.com/CareBoo/unity-algorand-sdk/commit/e05a1b36fe5fce154862308569c212b52c5a4c0f))
- **indexer:** :sparkles: implement `Transaction.AssetConfiguration` model fields ([b021ac9](https://github.com/CareBoo/unity-algorand-sdk/commit/b021ac9f7fe4800900e0bb73efc5bc3be7228c93))
- **indexer:** :sparkles: implement `Transaction.AssetFreeze` model fields ([655aee9](https://github.com/CareBoo/unity-algorand-sdk/commit/655aee9fee97f43e4a8c669846924c86dbbe071c))
- **indexer:** :sparkles: implement `Transaction.AssetTransfer` model fields ([5322ad7](https://github.com/CareBoo/unity-algorand-sdk/commit/5322ad707f3d4f99dd182d0c6834209145e29e4d))
- **indexer:** :sparkles: implement `Transaction.KeyRegistration` ([da15081](https://github.com/CareBoo/unity-algorand-sdk/commit/da1508124e3564de206990d0dcb8c16a77f44742))
- **indexer:** :sparkles: implement `Transaction.KeyRegistration` model fields ([ab75259](https://github.com/CareBoo/unity-algorand-sdk/commit/ab75259643194eff90a0eeed7bb0c2bba98d1953))
- **indexer:** :sparkles: implement `TransactionResponse` ([0cdcdde](https://github.com/CareBoo/unity-algorand-sdk/commit/0cdcdde80cc9135e2f5326715f294713b05a1a20))
- **indexer:** :sparkles: implement `TransactionsQuery` ([a9e353a](https://github.com/CareBoo/unity-algorand-sdk/commit/a9e353adc44117479f7c5bb47d456880f0a5f164))
- **indexer:** :sparkles: implement AssetsQuery ([ebfb08c](https://github.com/CareBoo/unity-algorand-sdk/commit/ebfb08c00b4a95e01a0fe0a7bb2541d3bb612295))
- **indexer:** :sparkles: implement TransactionsResponse ([27345f5](https://github.com/CareBoo/unity-algorand-sdk/commit/27345f5a579485d040f3f7c7c166e0b32a7d6d39))
- **json:** :sparkles: implement `JsonReader.Skip` ([cba58d6](https://github.com/CareBoo/unity-algorand-sdk/commit/cba58d6ae81c5225d1c7379d7ebf5564c2a536e5))
- **json:** :sparkles: implement `JsonWriter.WriteRaw` ([f780932](https://github.com/CareBoo/unity-algorand-sdk/commit/f780932803e25f3641e55e636be00b6b371430d4))
- **json:** :sparkles: implement `ReadRaw` ([c2ec519](https://github.com/CareBoo/unity-algorand-sdk/commit/c2ec51903457a5872794b003d0b92e3e2fff8d90))
- **kmd:** :sparkles: add `Error` to `ErrorMessage` which is used for kmd ([b64aac6](https://github.com/CareBoo/unity-algorand-sdk/commit/b64aac6d959cf67a02827268dfe3ac56ec11b666))
- **kmd:** :sparkles: implement `APIV1DELETEKeyResponse` ([d273c12](https://github.com/CareBoo/unity-algorand-sdk/commit/d273c1280988fcbd4b7fc6a0aa43255464fb1e5e))
- **kmd:** :sparkles: implement `APIV1DELETEMultisigResponse` ([b323213](https://github.com/CareBoo/unity-algorand-sdk/commit/b323213bf13991365d8cfd9542aab37135421183))
- **kmd:** :sparkles: implement `APIV1GETWalletsResponse` ([cd7180d](https://github.com/CareBoo/unity-algorand-sdk/commit/cd7180d0fa05bd749fecd6623e0ad2bd2a02a132))
- **kmd:** :sparkles: implement `APIV1Wallet` ([4b74869](https://github.com/CareBoo/unity-algorand-sdk/commit/4b7486950aae352ff8d1119c88026d2a7ffe6a8a))
- **kmd:** :sparkles: implement `APIV1WalletHandle` ([da73b67](https://github.com/CareBoo/unity-algorand-sdk/commit/da73b672661e6ef53168b8cf610d3eed9b8262f7))
- **kmd:** :sparkles: implement `CreateWalletRequest` ([02cac0a](https://github.com/CareBoo/unity-algorand-sdk/commit/02cac0aabe7064a8c5a033d2eee44a198b6afeb6))
- **kmd:** :sparkles: implement `CreateWalletRequest` api fields ([9f1b667](https://github.com/CareBoo/unity-algorand-sdk/commit/9f1b6672861aae64f636bd6ec346d4efdea38f1a))
- **kmd:** :sparkles: implement `CreateWalletResponse` ([d0c3965](https://github.com/CareBoo/unity-algorand-sdk/commit/d0c396531c32ea1f277ae1d1883d08b718fdddc8))
- **kmd:** :sparkles: implement `DeleteKeyRequest` ([9de69d0](https://github.com/CareBoo/unity-algorand-sdk/commit/9de69d0a677e28008fa73e5c56957921a29bf8cb))
- **kmd:** :sparkles: implement `DeleteMultiSigRequest` ([4d0b859](https://github.com/CareBoo/unity-algorand-sdk/commit/4d0b859cea81428d39fc9e4aea29a28b3a4e9374))
- **kmd:** :sparkles: implement `ExportKeyRequest` ([2dfed57](https://github.com/CareBoo/unity-algorand-sdk/commit/2dfed577a7e6b1bf1ef276a5e2ddff57a4532681))
- **kmd:** :sparkles: implement `ExportKeyResponse.Equals` ([a5769c7](https://github.com/CareBoo/unity-algorand-sdk/commit/a5769c7ceb1e8f5f8e36fccb123653e1a1c6b9ae))
- **kmd:** :sparkles: implement `ExportKeyResponse` ([f5dc29a](https://github.com/CareBoo/unity-algorand-sdk/commit/f5dc29a2f34a1244adb3e475ea21cff67416a8b2))
- **kmd:** :sparkles: implement `ExportMasterKeyRequest` ([b668ac9](https://github.com/CareBoo/unity-algorand-sdk/commit/b668ac98790b2849194a4ce1bf63073bd2a93499))
- **kmd:** :sparkles: implement `ExportMasterKeyResponse` ([eaf3066](https://github.com/CareBoo/unity-algorand-sdk/commit/eaf30661e5cd0a3201ca9f1d0bfd1b1cb2aaf7a5))
- **kmd:** :sparkles: implement `ExportMultiSigRequest` ([eab6771](https://github.com/CareBoo/unity-algorand-sdk/commit/eab677148322ff5a012b919aeff67a8fc7b88fef))
- **kmd:** :sparkles: implement `ExportMultiSigResponse` ([41677c2](https://github.com/CareBoo/unity-algorand-sdk/commit/41677c24c781017801e5f62846714a70fb7bfdfc))
- **kmd:** :sparkles: implement `GenerateKeyResponse` ([82f0749](https://github.com/CareBoo/unity-algorand-sdk/commit/82f07495143755c2c13f9ee4a6bab708e67b537b))
- **kmd:** :sparkles: implement `GenerateMasterKeyRequest` ([41f2b64](https://github.com/CareBoo/unity-algorand-sdk/commit/41f2b64f34fd6294af51b97c96c6bbf1d0fad144))
- **kmd:** :sparkles: implement `ImportKeyRequest` ([09c8308](https://github.com/CareBoo/unity-algorand-sdk/commit/09c8308c0d00e58941a97ccb74af4b0142df002f))
- **kmd:** :sparkles: implement `ImportKeyResponse` ([338e9e7](https://github.com/CareBoo/unity-algorand-sdk/commit/338e9e7536f8dd3752200be7b762dfaee079e39e))
- **kmd:** :sparkles: implement `ImportMultiSigRequest` ([b8eb046](https://github.com/CareBoo/unity-algorand-sdk/commit/b8eb04619bbd9966452de5f48a76928400472fc8))
- **kmd:** :sparkles: implement `ImportMultiSigResponse` ([6a92419](https://github.com/CareBoo/unity-algorand-sdk/commit/6a924199ebc5ee9892630d9bfa14691a7efe7bfb))
- **kmd:** :sparkles: implement `InitWalletHandleTokenRequest` ([5c2bde4](https://github.com/CareBoo/unity-algorand-sdk/commit/5c2bde417793a0cc2c5ed74527841655affd8650))
- **kmd:** :sparkles: implement `InitWalletHandleTokenResponse` ([4eacfe2](https://github.com/CareBoo/unity-algorand-sdk/commit/4eacfe2654cc83e1f36c7dc2434c39e0175dfa09))
- **kmd:** :sparkles: implement `KmdClient` ([b37cef3](https://github.com/CareBoo/unity-algorand-sdk/commit/b37cef3c93d66110fe5b9dbca6723da3849ef68d)), closes [#13](https://github.com/CareBoo/unity-algorand-sdk/issues/13)
- **kmd:** :sparkles: implement `ListKeysRequest` ([70ce0ff](https://github.com/CareBoo/unity-algorand-sdk/commit/70ce0ff4da135cad5e66b2c7d49acb4ebe62b78b))
- **kmd:** :sparkles: implement `ListKeysResponse` ([6fb2a6d](https://github.com/CareBoo/unity-algorand-sdk/commit/6fb2a6d892e7b7a43cc4418fa507dd8c1ade9183))
- **kmd:** :sparkles: implement `ListMultiSigRequest` ([3da753e](https://github.com/CareBoo/unity-algorand-sdk/commit/3da753ea7606ce7be3e1292cde6a73311bae2009))
- **kmd:** :sparkles: implement `ListMultiSigResponse` ([d0a6782](https://github.com/CareBoo/unity-algorand-sdk/commit/d0a678261616ba5fffefac387da5bead2e953ded))
- **kmd:** :sparkles: implement `ListWalletsRequest` ([91de444](https://github.com/CareBoo/unity-algorand-sdk/commit/91de44473618235d4203bcc25c2b3a51fbd2563f))
- **kmd:** :sparkles: implement `ReleaseWalletHandleTokenRequest` ([c6a836d](https://github.com/CareBoo/unity-algorand-sdk/commit/c6a836dee7d85da7f3aa211fb2f3312f989d95b9))
- **kmd:** :sparkles: implement `ReleaseWalletHandleTokenResponse` ([3f2d081](https://github.com/CareBoo/unity-algorand-sdk/commit/3f2d08140e7c34b3377a361c85cc1bb93fbab1a0))
- **kmd:** :sparkles: implement `RenameWalletRequest` ([ccf7d3e](https://github.com/CareBoo/unity-algorand-sdk/commit/ccf7d3e824ad523ab92ac31bdca551aee39f1089))
- **kmd:** :sparkles: implement `RenameWalletResponse` ([1d35264](https://github.com/CareBoo/unity-algorand-sdk/commit/1d352644c714c664024ffcf7c1de2b2e7a14a272))
- **kmd:** :sparkles: implement `RenewWalletHandleTokenRequest` ([76269cc](https://github.com/CareBoo/unity-algorand-sdk/commit/76269ccb2474b826926fe5102cd8425aed814aab))
- **kmd:** :sparkles: implement `RenewWalletHandleTokenResponse` ([d4f6a9a](https://github.com/CareBoo/unity-algorand-sdk/commit/d4f6a9a10e8cca5a9c89fc27cd55c3760c43dad8))
- **kmd:** :sparkles: implement `SignMultiSigRequest` ([d535391](https://github.com/CareBoo/unity-algorand-sdk/commit/d535391d4cfa397ba6d56cf766966299a54beb59))
- **kmd:** :sparkles: implement `SignMultiSigResponse` ([0b16ac4](https://github.com/CareBoo/unity-algorand-sdk/commit/0b16ac485c27b9caf6efb58604cec90f6f4c2b27))
- **kmd:** :sparkles: implement `SignProgramMultiSigRequest` ([9460af9](https://github.com/CareBoo/unity-algorand-sdk/commit/9460af994e14ff3e3aae2317d1fba2fbece38388))
- **kmd:** :sparkles: implement `SignProgramMultiSigResponse` ([ee5ea0e](https://github.com/CareBoo/unity-algorand-sdk/commit/ee5ea0e8bf2f999aa2f340f31fe9d9f4d980b0a8))
- **kmd:** :sparkles: implement `SignProgramRequest` ([c8826fa](https://github.com/CareBoo/unity-algorand-sdk/commit/c8826fa39de84df78d2dc8e241145ec4f4811429))
- **kmd:** :sparkles: implement `SignTransactionRequest` ([38ec667](https://github.com/CareBoo/unity-algorand-sdk/commit/38ec667c31f3b3b2e8309196b3f57f27a89e280d))
- **kmd:** :sparkles: implement `SignTransactionResponse` ([e88c293](https://github.com/CareBoo/unity-algorand-sdk/commit/e88c2933f2c89a86669edbc3560ad20f8bd01a81))
- **kmd:** :sparkles: implement `VersionsRequest` ([90f9cb6](https://github.com/CareBoo/unity-algorand-sdk/commit/90f9cb65238272c5b7ef9c468f57201bd95aad27))
- **kmd:** :sparkles: implement `VersionsResponse` ([15ab570](https://github.com/CareBoo/unity-algorand-sdk/commit/15ab570f68f1a8170cac5678dd5f5cb400b28097))
- **kmd:** :sparkles: implement `WalletInfoResponse` ([cb073e9](https://github.com/CareBoo/unity-algorand-sdk/commit/cb073e99625cde9075a312c56ed7cc5e8a288c4c))
- **kmd:** implement `SignProgramResponse` ([f529e2a](https://github.com/CareBoo/unity-algorand-sdk/commit/f529e2a85538ff6e3d13ebe37a23d44cce4aa4b7))
- **msgpack:** :sparkles: implement `MessagePackReader.ReadRaw` ([0f6e28a](https://github.com/CareBoo/unity-algorand-sdk/commit/0f6e28afa94261b8af955e1c9fe9f6bdd62f2e64))
- **msgpack:** :sparkles: implement `MessagePackWriter.WriteRaw` ([703544c](https://github.com/CareBoo/unity-algorand-sdk/commit/703544c00fbcb5cd5ce0def75e5d921e6a7a922c))
- **networking:** :sparkles: add `ApiResponse` API to `ApiResponse<>` ([b9728f9](https://github.com/CareBoo/unity-algorand-sdk/commit/b9728f9d43d1e55b8732c8132bd6b6015c395a84))
- **serialization:** :sparkles: add formatters for `PrivateKey`, `PublicKey`, and `Signature` ([11d345b](https://github.com/CareBoo/unity-algorand-sdk/commit/11d345b839abc76cc1fe376cfed833a2d2041be6))
- **serialization:** :sparkles: add support for deserializing `byte[]` ([6764157](https://github.com/CareBoo/unity-algorand-sdk/commit/676415734c8e1d4884927399c292526ec01e2361))
- **serialization:** :sparkles: add support for generically serializing byte enums to strings ([bae5d83](https://github.com/CareBoo/unity-algorand-sdk/commit/bae5d830cc420d3a6033e34baa988512cdb96bff))
- **serialization:** :sparkles: finish implementing `MessagePackReader.Integers` and `MessagePackWriter.Integers` ([d669359](https://github.com/CareBoo/unity-algorand-sdk/commit/d66935919094ef99208c425f27edf3c0223e0e2d))
- **serialization:** :sparkles: finish implementing serializer/deserializer methods in `EnumFormatter` ([414991f](https://github.com/CareBoo/unity-algorand-sdk/commit/414991fd35c1c2dac8988f0aa12822e9d01af540))
- **serialization:** :sparkles: implement `AlgoApiObject` ([9ef3335](https://github.com/CareBoo/unity-algorand-sdk/commit/9ef333546494d141ca66b5ebcf25ec18651c443e))
- **serialization:** :sparkles: implement `Timestamp` ([e520962](https://github.com/CareBoo/unity-algorand-sdk/commit/e5209622de596d71a88ab3ef9e4f8617d646d548))
- **serialization:** :tada: update formatter cache ([ae346d6](https://github.com/CareBoo/unity-algorand-sdk/commit/ae346d6b9408fd66815de9d2a6f461a453ef890e))
- **transaction:** :sparkles: add `GetSignature` and `ToRaw` extensions to `ITransaction` ([426dea6](https://github.com/CareBoo/unity-algorand-sdk/commit/426dea611a06178a0e11059f359034c10a98287d))
- **transaction:** :sparkles: add `RegisterAccountOnline` and `RegisterAccountOffline` APIs ([d52e953](https://github.com/CareBoo/unity-algorand-sdk/commit/d52e95341acb07ad4412634f7c0002035668e441))
- **transaction:** :sparkles: add `Transaction.AppCreate` and `Transaction.AppConfigure` APIs ([0dd2e75](https://github.com/CareBoo/unity-algorand-sdk/commit/0dd2e751dfadb31ab1004e01e4cce3e0e57f943d))
- **transaction:** :sparkles: add `Transaction.AssetCreate` `Transaction.AssetConfigure` and `Transaction.AssetDelete` API ([4192aba](https://github.com/CareBoo/unity-algorand-sdk/commit/4192aba6962cb06a41a461e0fdf39a5d927c63ff))
- **transaction:** :sparkles: add `Transaction.AssetFreeze` API ([d8b5721](https://github.com/CareBoo/unity-algorand-sdk/commit/d8b5721a291a732b3ee94560e808e62d3eb32798))
- **transaction:** :sparkles: add `Transaction.Payment` API ([241ec4c](https://github.com/CareBoo/unity-algorand-sdk/commit/241ec4c33df16bb8fc4018ef95766ef2748b1214))
- **transaction:** :sparkles: implement address from application id ([a7df476](https://github.com/CareBoo/unity-algorand-sdk/commit/a7df4765b61ff1c0f89248686fa6f5148e90199b))
- **transaction:** :sparkles: implement AssetClawback and AssetAccept ([ccb2dc6](https://github.com/CareBoo/unity-algorand-sdk/commit/ccb2dc66fcbc66c738168a34b95d55aba4039945)), closes [#24](https://github.com/CareBoo/unity-algorand-sdk/issues/24) [#25](https://github.com/CareBoo/unity-algorand-sdk/issues/25)
- **transaction:** :sparkles: implement atomic transfers ([c97fdc1](https://github.com/CareBoo/unity-algorand-sdk/commit/c97fdc1d1fd60c684e6f4256ef53f32b55509370)), closes [#45](https://github.com/CareBoo/unity-algorand-sdk/issues/45)
- **transaction:** :sparkles: use `TransactionParams` in Transaction constructors ([c99bd3f](https://github.com/CareBoo/unity-algorand-sdk/commit/c99bd3f6dad2270bdcec2f5551ac0bb4f21cd556))
- **unity:** make AlgoApiClient's Serializable ([a6ef8a4](https://github.com/CareBoo/unity-algorand-sdk/commit/a6ef8a45f08256576a38013229bf9e50114d36d8))

### Reverts

- **demo:** :fire: remove bossroom assets... too big to be a sample ([d768029](https://github.com/CareBoo/unity-algorand-sdk/commit/d76802948df36cc557744eaa889de43d2d253707))

### BREAKING CHANGES

- Removing `NativeSliceExtensions`
- **algod:** `AccountParticipation` no longer exists
- **transaction:** All transaction constructors have been changed to use `TransactionParams`
- **indexer:** Removed token from IndexerClient constructor
- **kmd:** changes `IKmdClient` API to use parameters instead of `Request` objs
- **indexer:** indexer queries are now given as optional arguments
- remove `Transaction.VerifySignature`, `SignedTransaction.Verify`
- signed transactions are now represented by `SignedTransaction` (non-generic)
- renamed `RawSignedTransaction` -> `SignedTransaction`
- `RawTransaction` renamed to `Transaction`
- AlgoApiKeyAttribute -> AlgoApiFieldAttribute
- **algod:** `Block` renamed to `BlockResponse`
- Replace Buffer with GetUnsafePtr
- Algo Serializer API has completely changed...
- Removes the `SendTransactionRaw` method from `AlgodClient`

# [1.0.0-pre.7](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-pre.6...v1.0.0-pre.7) (2021-10-29)

### Bug Fixes

- **transaction:** remove unused assetCloseTo param in `AssetClawback` ([f45dac1](https://github.com/CareBoo/unity-algorand-sdk/commit/f45dac1328af34e1eb437431e2bc60aa71ad53af))

# [1.0.0-pre.6](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-pre.5...v1.0.0-pre.6) (2021-10-28)

### Bug Fixes

- **indexer:** fix missing `Version` field in `HealthCheck` ([39c3444](https://github.com/CareBoo/unity-algorand-sdk/commit/39c3444ee0a6a628dbd80619804a12720223a2ef))

### Features

- **algod:** add support for sending a group of transactions ([a7a7695](https://github.com/CareBoo/unity-algorand-sdk/commit/a7a7695ce1e0196ca5bc3fe47e463b6a0c643f2f))

# [1.0.0-pre.5](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-pre.4...v1.0.0-pre.5) (2021-10-27)

### Bug Fixes

- **crypto:** fix libsodium not working on OSX ([1154450](https://github.com/CareBoo/unity-algorand-sdk/commit/1154450267b71762195f701694616a38d47fd529))

# [1.0.0-pre.4](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-pre.3...v1.0.0-pre.4) (2021-10-27)

### Features

- **unity:** make AlgoApiClient's Serializable ([4a3aa11](https://github.com/CareBoo/unity-algorand-sdk/commit/4a3aa11fe3ecdf46f00ef29829e1d965043ee3aa))

# [1.0.0-pre.3](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-pre.2...v1.0.0-pre.3) (2021-10-25)

### Code Refactoring

- :fire: remove `NativeSliceExtensions` ([c5c7715](https://github.com/CareBoo/unity-algorand-sdk/commit/c5c77158ab8bfa6585e95f3cb01594b80758f635))

### BREAKING CHANGES

- Removing `NativeSliceExtensions`

# [1.0.0-pre.2](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-pre.1...v1.0.0-pre.2) (2021-10-25)

### Bug Fixes

- fix empty dirs finding their way into the project ([82afc9d](https://github.com/CareBoo/unity-algorand-sdk/commit/82afc9d14a0374b39e63dd7581b950324e12bf39))

# 1.0.0-pre.1 (2021-10-24)

### Bug Fixes

- :bug: fix `RawTransaction.Equals` ([f6bd889](https://github.com/CareBoo/unity-algorand-sdk/commit/f6bd889c737f970c835d3273f7c28b002794ed3f))
- :bug: fix compile errs ([7a03ed9](https://github.com/CareBoo/unity-algorand-sdk/commit/7a03ed99a5cd7d3657df89c58a989ee0e33ced66))
- :bug: fix crash on ArrayComparer.Equals ([8ca1b06](https://github.com/CareBoo/unity-algorand-sdk/commit/8ca1b06daedcbdf97650f4b79bd72a134238bec0))
- :bug: fix issues with codegen and AOT compilation ([d2f9bdc](https://github.com/CareBoo/unity-algorand-sdk/commit/d2f9bdc8f8c77b8afb690162c3fafda81b5f8c48))
- :bug: fix keyworded enum types formatting as numbers ([d686c13](https://github.com/CareBoo/unity-algorand-sdk/commit/d686c134dff5e0d657f974600c5948b86f506f0f))
- :bug: Fix npm release ([9e68078](https://github.com/CareBoo/unity-algorand-sdk/commit/9e68078b5f7f75a7670e7cf03dc1ee5137709bf1))
- :bug: Fix repository ([e765589](https://github.com/CareBoo/unity-algorand-sdk/commit/e765589b92849e23b7d080f335067cba1c1a8b62))
- :bug: fix signatures ([2fb4614](https://github.com/CareBoo/unity-algorand-sdk/commit/2fb461459ac58266c1078bdb16607d65a25d8553))
- :triangular_flag_on_post: Update package registry ([a0800bc](https://github.com/CareBoo/unity-algorand-sdk/commit/a0800bc57c6fde758182075a90e2fbd96ce67be8))
- :white_check_mark: Fix CI tests on Github Actions can't find libsodium ([20c4ad5](https://github.com/CareBoo/unity-algorand-sdk/commit/20c4ad56edf57cef595c2f4078bba4e15d893602)), closes [#12](https://github.com/CareBoo/unity-algorand-sdk/issues/12)
- **algod:** :bug: fix `AlgodClient.TealCompile` ([8f5a67a](https://github.com/CareBoo/unity-algorand-sdk/commit/8f5a67a694cd21496b6db7c1630324ddbfd13f1f)), closes [#20](https://github.com/CareBoo/unity-algorand-sdk/issues/20)
- **algod:** :bug: fix `AssetParams` serialization ([cbf9a69](https://github.com/CareBoo/unity-algorand-sdk/commit/cbf9a6981c52c565eb3837186c38765e5b8bea27))
- **algod:** :bug: fix pending transactions not returning messagepack ([dca09f8](https://github.com/CareBoo/unity-algorand-sdk/commit/dca09f8d6c2e1781097f3386355852b7179f01d9))
- **algod:** :bug: fix vrfpubkey formatter lookup ([0bb0014](https://github.com/CareBoo/unity-algorand-sdk/commit/0bb001407b81abc288d271cda3c6d6b9c19c49df))
- **algod:** :bug: fix vrfpubkey formatting ([7d02443](https://github.com/CareBoo/unity-algorand-sdk/commit/7d024437ac9415449717ebf8a97017fcc10f9ee1))
- **algod:** :bug: replace `VrfPubKey` with `FixedString128Bytes` ([94b02b0](https://github.com/CareBoo/unity-algorand-sdk/commit/94b02b06f25d4b97e264f085cc96afbfdfb68862)), closes [#42](https://github.com/CareBoo/unity-algorand-sdk/issues/42)
- **indexer:** :bug: add missing fields to `Account` model ([5732a1d](https://github.com/CareBoo/unity-algorand-sdk/commit/5732a1d7b6a57d300e8e8b4662696e180c0c2ecb))
- **indexer:** :bug: add token back to indexer ([023cb05](https://github.com/CareBoo/unity-algorand-sdk/commit/023cb05f173242f2c3f234f8203fcf2c03ea5369))
- **indexer:** :bug: fix `HealthCheck` missing serialization logic ([f5165c6](https://github.com/CareBoo/unity-algorand-sdk/commit/f5165c681387450a791f191a2ae88a060741a9de))
- **indexer:** :bug: fix indexer requires token ([57f6114](https://github.com/CareBoo/unity-algorand-sdk/commit/57f61141f9c7763bb10a3a9deac07dcbc65ea6a9))
- **indexer:** :bug: fix transaction formatters missing valid msgpack fields ([bf917ac](https://github.com/CareBoo/unity-algorand-sdk/commit/bf917ac30f964b9102c293d00a60d6f0072e75bd))
- **indexer:** :fire: fix huge issue with indexer where query parameters were in body ([7bcca20](https://github.com/CareBoo/unity-algorand-sdk/commit/7bcca2022b8c1238cd35f225fccef80ada562143))
- **indexer:** :sparkles: fix `TealValue` msgpack fieldnames ([2a6ef27](https://github.com/CareBoo/unity-algorand-sdk/commit/2a6ef27e4f465c73a3cb744eae12267929c06f07))
- **json:** :bug: fix `PrivateKey` JSON deserialization err ([cb327e0](https://github.com/CareBoo/unity-algorand-sdk/commit/cb327e0f38ef61e43f5c5e38c3090ab02b83693d))
- **json:** :bug: fix empty json objects missing begin object '{' char ([af9a2f1](https://github.com/CareBoo/unity-algorand-sdk/commit/af9a2f1cdc7b02a16e1ef052a1156ddee0d0a2a8))
- **kmd:** :art: fix `KmdClient` API incorrectly using optional args ([a53998b](https://github.com/CareBoo/unity-algorand-sdk/commit/a53998b00aa103a47ad1fea014c5f6627f8a3d34)), closes [#36](https://github.com/CareBoo/unity-algorand-sdk/issues/36)
- **kmd:** :bug: fix `GenerateKeyRequest` to use `wallet_handle_token` not password ([a86c25e](https://github.com/CareBoo/unity-algorand-sdk/commit/a86c25ea9b233dd755f234d64d36467415ce6ebb))
- **kmd:** :bug: fix `ImportKeyRequest` to use wallet_handle_token not password ([f020a03](https://github.com/CareBoo/unity-algorand-sdk/commit/f020a03fb19c9cf89452066d6dbd3cf278eb9f7f))
- **kmd:** :bug: fix `KmdClient.SignTransaction` returning a signed transaction message ([f0a354c](https://github.com/CareBoo/unity-algorand-sdk/commit/f0a354caeafd05ae7a984451582e8d53b82bbd6b))
- **kmd:** :bug: fix `WalletHandle` not added to formatter cache ([4c30939](https://github.com/CareBoo/unity-algorand-sdk/commit/4c309395b3f7076aa4db1e6ebfb552d59fad57b0))
- **kmd:** :bug: fix multisig ([fc7bcd6](https://github.com/CareBoo/unity-algorand-sdk/commit/fc7bcd6f647c399b3edebb52ccd65fccf715393e))
- **serialization:** :bug: fix `Block` not having a formatter ([dec6176](https://github.com/CareBoo/unity-algorand-sdk/commit/dec61762ddfba5c7f43223175c8c63f2d0b3609e))
- **serialization:** :bug: fix `BlockResponse` Serialization ([1252a16](https://github.com/CareBoo/unity-algorand-sdk/commit/1252a1634d6bb2fa00f78c1628ce7dbf33da6a56))
- **serialization:** :bug: fix `JsonWriter` not writing to `NativeText` ([2b61659](https://github.com/CareBoo/unity-algorand-sdk/commit/2b61659ea6801947f7b31069ecade0d6cb5f1e09))
- **serialization:** :bug: fix `SignedTransaction` serialization ([f4bde8d](https://github.com/CareBoo/unity-algorand-sdk/commit/f4bde8d4fd65e6146df35a6ce0890ed4417a235a))
- **serialization:** :bug: fix `TransactionId` serialization ([b7e07ca](https://github.com/CareBoo/unity-algorand-sdk/commit/b7e07caf5e8b8b5b0327c15c618a75263726dc18))
- **serialization:** :bug: fix discrepancy between `TransactionId` and `TransactinIdResponse` ([d0c0370](https://github.com/CareBoo/unity-algorand-sdk/commit/d0c0370c0578e6d2296d0667557148e711c67174))
- **serialization:** :bug: fix issue where fixed strings were incorrectly being added to the queryparams ([a3e6fd9](https://github.com/CareBoo/unity-algorand-sdk/commit/a3e6fd9dbaf62d6e4bb05432568c0de36e3abb35))
- **serialization:** :bug: fix missing `AlgoApiObject` on all Transaction Params ([a11015f](https://github.com/CareBoo/unity-algorand-sdk/commit/a11015ff3da75db5ab5d6f7c809a89f50fe1142b))
- **transaction:** :bug: fix `OnCompletion` having incorrect byte values ([514a378](https://github.com/CareBoo/unity-algorand-sdk/commit/514a378dce9c0669002960f6ef76d453a54fe0f0))
- **transaction:** :bug: fix incorrect constructor in `Transaction.ApplicationCall` ([98b5829](https://github.com/CareBoo/unity-algorand-sdk/commit/98b58295d7676d73a924a5d837b7f9b0dc8a62c9))

### Code Refactoring

- :fire: remove some verify methods in the transaction ([70a8308](https://github.com/CareBoo/unity-algorand-sdk/commit/70a830830d3778a23d32fd9985d5d424972dd99f))
- :fire: remove unnecessary `SendTransactionRaw` ([09b3bdc](https://github.com/CareBoo/unity-algorand-sdk/commit/09b3bdc0dfbeb2b47603b3cfc92be0478120bd09))
- :recycle: convert all `SignedTransaction<>` to a single `SignedTransaction` ([26fa65a](https://github.com/CareBoo/unity-algorand-sdk/commit/26fa65a53fa4cb2f4e8d81aa9ec2adc537d09b01))
- :recycle: rename `RawSignedTransaction` -> `SignedTransaction` ([059c6a3](https://github.com/CareBoo/unity-algorand-sdk/commit/059c6a369bd3f5e6d2b65965927cffd8bce65a49))
- :recycle: rename `RawTransaction` -> `Transaction` ([bade09e](https://github.com/CareBoo/unity-algorand-sdk/commit/bade09e7edc7df1d6dcbeb932f38fe7c199b21bb))
- :recycle: rename AlgoApiKeyAttribute -> AlgoApiFieldAttribute ([1b37028](https://github.com/CareBoo/unity-algorand-sdk/commit/1b370289ba2ffe8457f35c998aed6efcfe01931c))
- **algod:** :recycle: rename `Block` -> `BlockResponse` ([eca9fb6](https://github.com/CareBoo/unity-algorand-sdk/commit/eca9fb6c2be3a99272067d308a089148a1680427))
- **algod:** :recycle: replace `AccountParticipation` with `Transaction.KeyRegistration.Params` ([3b624ee](https://github.com/CareBoo/unity-algorand-sdk/commit/3b624eec4974e28b0cc31b019d41f37dc60207ca))
- **kmd:** :recycle: replace `Request` with explicit method params ([030a0a9](https://github.com/CareBoo/unity-algorand-sdk/commit/030a0a9486f6270e6e867ad1efbad73b90e0ecfb))
- remove .NET 4.8 requirement ([9f55707](https://github.com/CareBoo/unity-algorand-sdk/commit/9f55707ba4f8a3d9b12fb7530b6ceec40c95219b)), closes [#19](https://github.com/CareBoo/unity-algorand-sdk/issues/19) [#9](https://github.com/CareBoo/unity-algorand-sdk/issues/9)

### Features

- :construction_worker: Add npmjs support ([56f60f0](https://github.com/CareBoo/unity-algorand-sdk/commit/56f60f0804c3cac377c78a5c47eea17785b29f9e))
- :heavy_plus_sign: Add libsodium ios, android, and windows ([00c9511](https://github.com/CareBoo/unity-algorand-sdk/commit/00c95113a152f01e737e56be6a31b3207aed57ff))
- :sparkles: Add Account.Generate ([95233bf](https://github.com/CareBoo/unity-algorand-sdk/commit/95233bf877ee70d6e67f91baaf6f9e640b0a9141))
- :sparkles: Add Address struct ([d92b903](https://github.com/CareBoo/unity-algorand-sdk/commit/d92b90356cbf0070dd737a838eae417212a84a18))
- :sparkles: Add basic signed transaction support ([bb5900f](https://github.com/CareBoo/unity-algorand-sdk/commit/bb5900fa66bc9501c3d9da26e1e984ddd1fb2cc0))
- :sparkles: Add basic transaction support for message pack serialization and deserialization ([63243ed](https://github.com/CareBoo/unity-algorand-sdk/commit/63243ed492919522a752784756de2633d17a79fa))
- :sparkles: Add checksums ([2bc2189](https://github.com/CareBoo/unity-algorand-sdk/commit/2bc2189174e48de3324aa8ca4e3bbce22ca4e1c8))
- :sparkles: add FixedBytesFormatter ([941eb3f](https://github.com/CareBoo/unity-algorand-sdk/commit/941eb3f83b4b82d58431bc49b7f402e1ecfbccea))
- :sparkles: Add IEquatable, GetHashCode for RawTransaction ([885928e](https://github.com/CareBoo/unity-algorand-sdk/commit/885928e3c69dbc3742d11369d5e316853d6a8e8c))
- :sparkles: add LogicSig implementation ([51a21e0](https://github.com/CareBoo/unity-algorand-sdk/commit/51a21e02f98bb221ca2e14cbb8d1243f7b15b5ef))
- :sparkles: Add Mnemonic.FromString and Mnemonic.ToString ([d3a88c6](https://github.com/CareBoo/unity-algorand-sdk/commit/d3a88c6026ff3162a46f1e3d13ca54d103d43aaf))
- :sparkles: add readOnly support ([6070565](https://github.com/CareBoo/unity-algorand-sdk/commit/6070565057130a23b52575126774a6d1fa58718a))
- :sparkles: Add Sha512 interop ([86aabf5](https://github.com/CareBoo/unity-algorand-sdk/commit/86aabf5b0936cea36881cfaad1e1d87245755c92))
- :sparkles: Add start of Native Integration with Libsodium ([55e325f](https://github.com/CareBoo/unity-algorand-sdk/commit/55e325f4249603da679a66ece605f6ba8d5d762d))
- :sparkles: Added Transaction Header, Payment ([d3ebced](https://github.com/CareBoo/unity-algorand-sdk/commit/d3ebcedd3f86bdfabbb9f90a97e655fcadac1145))
- :sparkles: implement asset configuration transaction ([4f9ec58](https://github.com/CareBoo/unity-algorand-sdk/commit/4f9ec587932ed51da9bbbeaa1ddf3653a346c978)), closes [#27](https://github.com/CareBoo/unity-algorand-sdk/issues/27)
- :sparkles: implement AssetFreezeTransaction ([e3233bf](https://github.com/CareBoo/unity-algorand-sdk/commit/e3233bf9c8258eab91e8141c561d837f26780172)), closes [#26](https://github.com/CareBoo/unity-algorand-sdk/issues/26)
- :sparkles: implement AssetTransfer Transaction ([62a1841](https://github.com/CareBoo/unity-algorand-sdk/commit/62a1841332811753f48ac7fdccb1eec117607f3a)), closes [#23](https://github.com/CareBoo/unity-algorand-sdk/issues/23)
- :sparkles: update AlgoApiFormatterLookup ([cc112e5](https://github.com/CareBoo/unity-algorand-sdk/commit/cc112e55d4489e30afd2854757e482cf9d37c8ee))
- Add Mnemonic and Key datastructs ([4370f74](https://github.com/CareBoo/unity-algorand-sdk/commit/4370f74c4e5f5ed9977dc4954313bf57e7023547))
- Add signed transaction support for payment transactions ([c07d370](https://github.com/CareBoo/unity-algorand-sdk/commit/c07d3705f70bcc33c245aa120c667a7dcc28d6e1))
- **algod:** implement Algod Client ([a7c9e90](https://github.com/CareBoo/unity-algorand-sdk/commit/a7c9e90e2f61a715488bfd8f0095c0206ae6e739)), closes [#10](https://github.com/CareBoo/unity-algorand-sdk/issues/10) [#16](https://github.com/CareBoo/unity-algorand-sdk/issues/16) [#18](https://github.com/CareBoo/unity-algorand-sdk/issues/18)
- **indexer:** :recycle: rename `ApplicationStateSchema` -> `StateSchema` ([7b02f70](https://github.com/CareBoo/unity-algorand-sdk/commit/7b02f70dbac6c3a67a0168107103a43be0a262ad))
- **indexer:** :sparkles: add `IIndexerClient` ([093153e](https://github.com/CareBoo/unity-algorand-sdk/commit/093153e104a09e4d9f3cafe27ea494d44fe35840))
- **indexer:** :sparkles: implement `AccountQuery` ([bda5301](https://github.com/CareBoo/unity-algorand-sdk/commit/bda5301687bf0a90d51860cd96074118639cc722))
- **indexer:** :sparkles: implement `AccountResponse` ([09d7fb9](https://github.com/CareBoo/unity-algorand-sdk/commit/09d7fb9b53afba88181a98ee66a4f95633ae7257))
- **indexer:** :sparkles: implement `AccountsQuery` ([e7d56e2](https://github.com/CareBoo/unity-algorand-sdk/commit/e7d56e29b559af8288e334e4006fe44bbab496b6))
- **indexer:** :sparkles: implement `AccountsResponse` ([ea01260](https://github.com/CareBoo/unity-algorand-sdk/commit/ea01260b0cc63fb0ee873b3f62c0a243fe20da30))
- **indexer:** :sparkles: implement `AddressRole` ([3325b60](https://github.com/CareBoo/unity-algorand-sdk/commit/3325b60757eac9400e5bf227639c2946b8f36a2d))
- **indexer:** :sparkles: implement `Application` ([144d04d](https://github.com/CareBoo/unity-algorand-sdk/commit/144d04d940c2361b636e3cff21a6134b08515980))
- **indexer:** :sparkles: implement `ApplicationLocalState` ([9ad284f](https://github.com/CareBoo/unity-algorand-sdk/commit/9ad284f734a27fb2cfc61a203f9400f397f63d31))
- **indexer:** :sparkles: implement `ApplicationQuery` ([de02370](https://github.com/CareBoo/unity-algorand-sdk/commit/de02370084ddf28ebf5473fd9016d0f634537c24))
- **indexer:** :sparkles: implement `ApplicationResponse` ([fa34b20](https://github.com/CareBoo/unity-algorand-sdk/commit/fa34b201d734a2deae62731ce224841ea9c00895))
- **indexer:** :sparkles: implement `ApplicationsQuery` ([914ca8b](https://github.com/CareBoo/unity-algorand-sdk/commit/914ca8b4b483f25d91be6e59b953a0d3b3fb2426))
- **indexer:** :sparkles: implement `ApplicationsResponse` ([e342cdc](https://github.com/CareBoo/unity-algorand-sdk/commit/e342cdc68d34e2582b0965af801eed60569fbd5e))
- **indexer:** :sparkles: implement `Asset` ([161f361](https://github.com/CareBoo/unity-algorand-sdk/commit/161f3618265027bf37f025c90326b3b96df24e8f))
- **indexer:** :sparkles: implement `AssetHolding` ([203785a](https://github.com/CareBoo/unity-algorand-sdk/commit/203785a112052f38c796bb6d9a5cef3a884cdbac))
- **indexer:** :sparkles: implement `AssetParams` ([ac78bfe](https://github.com/CareBoo/unity-algorand-sdk/commit/ac78bfedb61875fabb480e602eb9866f37bdd926))
- **indexer:** :sparkles: implement `AssetQuery` ([c8eda94](https://github.com/CareBoo/unity-algorand-sdk/commit/c8eda9423f316cc766f02e88ce6a0dea5805bde5))
- **indexer:** :sparkles: implement `AssetResponse` ([cd8e48f](https://github.com/CareBoo/unity-algorand-sdk/commit/cd8e48ff76f0959f848fc1dc5eb4f6cede1a6afd))
- **indexer:** :sparkles: implement `AssetsResponse` ([ade022d](https://github.com/CareBoo/unity-algorand-sdk/commit/ade022d73f1d0557b2259c676a5b3fd1493b8fb5))
- **indexer:** :sparkles: implement `BalancesQuery` ([b8f167e](https://github.com/CareBoo/unity-algorand-sdk/commit/b8f167eeea4dceb2493accf20dac409a0903950b))
- **indexer:** :sparkles: implement `BalancesResponse` ([c7ae299](https://github.com/CareBoo/unity-algorand-sdk/commit/c7ae29964bb26898f5d86519167eacd247265017))
- **indexer:** :sparkles: implement `Block` ([2533f24](https://github.com/CareBoo/unity-algorand-sdk/commit/2533f240584fe2022c4a2d64d90ebf2f9555da59))
- **indexer:** :sparkles: implement `BlockRewards` ([bb49cb0](https://github.com/CareBoo/unity-algorand-sdk/commit/bb49cb05bdade80cb283d9624eb751d9fd9bd112))
- **indexer:** :sparkles: implement `BlockUpgradeStatus` ([e9a0423](https://github.com/CareBoo/unity-algorand-sdk/commit/e9a0423125b1e2f2d5c2a0794cd9c705f84667c4))
- **indexer:** :sparkles: implement `BlockUpgradeVote` ([de5e72e](https://github.com/CareBoo/unity-algorand-sdk/commit/de5e72e097ee4abff6db3d0fe6bf1b5ba0067c6a))
- **indexer:** :sparkles: implement `ErrorResponse` msgpack fields ([358e6a6](https://github.com/CareBoo/unity-algorand-sdk/commit/358e6a6475f716e0df55ed75ee8f0173c885d1c1))
- **indexer:** :sparkles: implement `EvalDelta` msgpack fields ([45b2df2](https://github.com/CareBoo/unity-algorand-sdk/commit/45b2df29af0e15759fafab566ca52b320f5e3adc))
- **indexer:** :sparkles: implement `EvalDeltaKeyValue` msgpack fields ([bd3c230](https://github.com/CareBoo/unity-algorand-sdk/commit/bd3c2304f227c5e252f6b269d3c33468e9e796a3))
- **indexer:** :sparkles: implement `HealthCheck` ([a164577](https://github.com/CareBoo/unity-algorand-sdk/commit/a1645779a235d1e8c5201af61671292065f37cda))
- **indexer:** :sparkles: implement `IndexerClient.GetAccount` ([88ba11a](https://github.com/CareBoo/unity-algorand-sdk/commit/88ba11ac8d2dbf0d1aae786bb7f904a3c872fc53))
- **indexer:** :sparkles: implement `IndexerClient.GetAccounts` ([2e427f1](https://github.com/CareBoo/unity-algorand-sdk/commit/2e427f11f1a834d3d63c9f3eb3090f4330136342))
- **indexer:** :sparkles: implement `IndexerClient.GetHealth` ([0883ff0](https://github.com/CareBoo/unity-algorand-sdk/commit/0883ff0b17fb2123bf251ffbe0ada58f25cf4ecc))
- **indexer:** :sparkles: implement `IndexerClient` ([d7fd35a](https://github.com/CareBoo/unity-algorand-sdk/commit/d7fd35a195b216ffef6371ba099355b51a85f852))
- **indexer:** :sparkles: implement `LogicSig` json fields ([b22297c](https://github.com/CareBoo/unity-algorand-sdk/commit/b22297c25e71bbee318e779281904090d2cb79d7))
- **indexer:** :sparkles: implement `MiniAssetHolding` ([59941b8](https://github.com/CareBoo/unity-algorand-sdk/commit/59941b8002cfbcb37a24b1a80ecb96dc9cf9620f))
- **indexer:** :sparkles: implement `MultiSig.SubSignature` ([ef526ec](https://github.com/CareBoo/unity-algorand-sdk/commit/ef526ec427cb3e280ad7336ad6f65c748070a577))
- **indexer:** :sparkles: implement `MultiSig` ([df959a7](https://github.com/CareBoo/unity-algorand-sdk/commit/df959a75a1898605840402cc2805b5fd2b0cc4f9))
- **indexer:** :sparkles: implement `OnCompletion` ([04227b9](https://github.com/CareBoo/unity-algorand-sdk/commit/04227b95db85508f8f258963454a3095263dd80e))
- **indexer:** :sparkles: implement `TealKeyValue` msgPack fields ([e521553](https://github.com/CareBoo/unity-algorand-sdk/commit/e5215533bc5a0c25f71fbc38584b35d3a9b910b2))
- **indexer:** :sparkles: implement `Transaction.ApplicationCall` ([5a5e819](https://github.com/CareBoo/unity-algorand-sdk/commit/5a5e819ec408c44a0999f38db7bab3b4c16c2d0e))
- **indexer:** :sparkles: implement `Transaction.ApplicationCall` model fields ([69d64e8](https://github.com/CareBoo/unity-algorand-sdk/commit/69d64e8b14d0b6bf3da90ce4e782d13c9364745a))
- **indexer:** :sparkles: implement `Transaction.AssetConfiguration` model fields ([baf7c2e](https://github.com/CareBoo/unity-algorand-sdk/commit/baf7c2ea37cdc40c9e890c7c84e9ae51478df06c))
- **indexer:** :sparkles: implement `Transaction.AssetFreeze` model fields ([be45b90](https://github.com/CareBoo/unity-algorand-sdk/commit/be45b901e4478e404cd6aac77996de78470b75b1))
- **indexer:** :sparkles: implement `Transaction.AssetTransfer` model fields ([84aa15e](https://github.com/CareBoo/unity-algorand-sdk/commit/84aa15eef120f882177b4f57800036dccb91a6b5))
- **indexer:** :sparkles: implement `Transaction.KeyRegistration` ([69e004e](https://github.com/CareBoo/unity-algorand-sdk/commit/69e004eb56759117e9fc7bdba8adc67d4ac30ddd))
- **indexer:** :sparkles: implement `Transaction.KeyRegistration` model fields ([44029ca](https://github.com/CareBoo/unity-algorand-sdk/commit/44029ca8048cd9baf31d1a9aff9eea3b335ec93d))
- **indexer:** :sparkles: implement `TransactionResponse` ([430a1fa](https://github.com/CareBoo/unity-algorand-sdk/commit/430a1fa205e194766d22aa565a552769bebfc919))
- **indexer:** :sparkles: implement `TransactionsQuery` ([9ec7f4f](https://github.com/CareBoo/unity-algorand-sdk/commit/9ec7f4f5ec8e8df560ae6ea4a6b6bb548c203680))
- **indexer:** :sparkles: implement AssetsQuery ([e8d6e55](https://github.com/CareBoo/unity-algorand-sdk/commit/e8d6e550bc757c03c4f5bbff484d9e17e5283b09))
- **indexer:** :sparkles: implement TransactionsResponse ([e1a46f8](https://github.com/CareBoo/unity-algorand-sdk/commit/e1a46f877be174bf7605b1b8dac8f146cf850c62))
- **json:** :sparkles: implement `JsonReader.Skip` ([0d6e6ab](https://github.com/CareBoo/unity-algorand-sdk/commit/0d6e6ab4c3b75e96c5e18b105019acb9c50180ac))
- **json:** :sparkles: implement `JsonWriter.WriteRaw` ([1643bad](https://github.com/CareBoo/unity-algorand-sdk/commit/1643badcf0069875a579da604ad0e2b5fb89d466))
- **json:** :sparkles: implement `ReadRaw` ([384abf8](https://github.com/CareBoo/unity-algorand-sdk/commit/384abf82e09f38724218d47675452c9f26595e21))
- **kmd:** :sparkles: add `Error` to `ErrorMessage` which is used for kmd ([3c37a94](https://github.com/CareBoo/unity-algorand-sdk/commit/3c37a947ed4ff34b2b008562e8a40e6eaa142078))
- **kmd:** :sparkles: implement `APIV1DELETEKeyResponse` ([81f6c1e](https://github.com/CareBoo/unity-algorand-sdk/commit/81f6c1e14cf23ca1bca6a6e22ee76a3196a26f6f))
- **kmd:** :sparkles: implement `APIV1DELETEMultisigResponse` ([4c1d6db](https://github.com/CareBoo/unity-algorand-sdk/commit/4c1d6db3848c387d244bbb624030cd0fb2614dec))
- **kmd:** :sparkles: implement `APIV1GETWalletsResponse` ([9025e9a](https://github.com/CareBoo/unity-algorand-sdk/commit/9025e9a2dbf20fa2ed951d5ba602017901257984))
- **kmd:** :sparkles: implement `APIV1Wallet` ([e514183](https://github.com/CareBoo/unity-algorand-sdk/commit/e5141835d7db31cb7d4274b0e4c80976ce40291c))
- **kmd:** :sparkles: implement `APIV1WalletHandle` ([8bae4b4](https://github.com/CareBoo/unity-algorand-sdk/commit/8bae4b4e3385ef981a9375a791cdd3cb5e867578))
- **kmd:** :sparkles: implement `CreateWalletRequest` ([63882eb](https://github.com/CareBoo/unity-algorand-sdk/commit/63882eb7f363f34e510629c41276b6deeeadc718))
- **kmd:** :sparkles: implement `CreateWalletRequest` api fields ([f32094f](https://github.com/CareBoo/unity-algorand-sdk/commit/f32094f9235dd2c09e32935cea80e747bd0af597))
- **kmd:** :sparkles: implement `CreateWalletResponse` ([2579231](https://github.com/CareBoo/unity-algorand-sdk/commit/2579231460707e0824699c897deb136e29e2755e))
- **kmd:** :sparkles: implement `DeleteKeyRequest` ([5c89319](https://github.com/CareBoo/unity-algorand-sdk/commit/5c8931958bf638375843e8b57ef484dae713d936))
- **kmd:** :sparkles: implement `DeleteMultiSigRequest` ([15afda0](https://github.com/CareBoo/unity-algorand-sdk/commit/15afda09b50ebf543a05b95c5970cc61f6e68d7d))
- **kmd:** :sparkles: implement `ExportKeyRequest` ([a8b1131](https://github.com/CareBoo/unity-algorand-sdk/commit/a8b11314d037e7e8ece0273dd82b27f511433582))
- **kmd:** :sparkles: implement `ExportKeyResponse.Equals` ([010e656](https://github.com/CareBoo/unity-algorand-sdk/commit/010e656a3ddd7a9abea101a86828ad6c7b635c45))
- **kmd:** :sparkles: implement `ExportKeyResponse` ([db01766](https://github.com/CareBoo/unity-algorand-sdk/commit/db01766b0ad407e9088a1f2d1ba8487da5500b31))
- **kmd:** :sparkles: implement `ExportMasterKeyRequest` ([2a8ed81](https://github.com/CareBoo/unity-algorand-sdk/commit/2a8ed81b3b0577329dc862cea9d4de7a7c7b59fb))
- **kmd:** :sparkles: implement `ExportMasterKeyResponse` ([2843847](https://github.com/CareBoo/unity-algorand-sdk/commit/2843847d11145f3ee143d6f1a072fd59999e32f3))
- **kmd:** :sparkles: implement `ExportMultiSigRequest` ([e67c925](https://github.com/CareBoo/unity-algorand-sdk/commit/e67c925bce69227c730971840ec07984cb14711b))
- **kmd:** :sparkles: implement `ExportMultiSigResponse` ([bac0edb](https://github.com/CareBoo/unity-algorand-sdk/commit/bac0edb70256ceb82916eac7c6e29ef2f52aaf69))
- **kmd:** :sparkles: implement `GenerateKeyResponse` ([1d6795f](https://github.com/CareBoo/unity-algorand-sdk/commit/1d6795ffdfa76e0c20f2ec56ba4dea36ac7303aa))
- **kmd:** :sparkles: implement `GenerateMasterKeyRequest` ([6503dd8](https://github.com/CareBoo/unity-algorand-sdk/commit/6503dd8ef729d2a5460bcded8fad46efea99e77a))
- **kmd:** :sparkles: implement `ImportKeyRequest` ([521f7c1](https://github.com/CareBoo/unity-algorand-sdk/commit/521f7c1af4183cfa00d4f6ad2e4e6d7ac81dbfde))
- **kmd:** :sparkles: implement `ImportKeyResponse` ([83798a6](https://github.com/CareBoo/unity-algorand-sdk/commit/83798a65359906741b908e752d83f73ae0273613))
- **kmd:** :sparkles: implement `ImportMultiSigRequest` ([5eb5d42](https://github.com/CareBoo/unity-algorand-sdk/commit/5eb5d42d7d67919d9b7357708e5d3a68861877d5))
- **kmd:** :sparkles: implement `ImportMultiSigResponse` ([001f3ea](https://github.com/CareBoo/unity-algorand-sdk/commit/001f3eaffcf58aa5050cb8c5632dfff300adf69d))
- **kmd:** :sparkles: implement `InitWalletHandleTokenRequest` ([36dd459](https://github.com/CareBoo/unity-algorand-sdk/commit/36dd459e9923e3539ef291e4fdf0f8ea4c3afc09))
- **kmd:** :sparkles: implement `InitWalletHandleTokenResponse` ([26db31b](https://github.com/CareBoo/unity-algorand-sdk/commit/26db31bea84cb50e16d0194e8bf7b147469d1971))
- **kmd:** :sparkles: implement `KmdClient` ([7433060](https://github.com/CareBoo/unity-algorand-sdk/commit/743306013c6d5d7473990b9c8ad4f5055b036ba0)), closes [#13](https://github.com/CareBoo/unity-algorand-sdk/issues/13)
- **kmd:** :sparkles: implement `ListKeysRequest` ([b55fe67](https://github.com/CareBoo/unity-algorand-sdk/commit/b55fe67f5dd3a3b5316bef1dd32ecd421081214a))
- **kmd:** :sparkles: implement `ListKeysResponse` ([0096bd5](https://github.com/CareBoo/unity-algorand-sdk/commit/0096bd59eeb9ed56e65e1abb5cdda262c63979dc))
- **kmd:** :sparkles: implement `ListMultiSigRequest` ([ce667d1](https://github.com/CareBoo/unity-algorand-sdk/commit/ce667d10e43c7cb55e2b6b247760bfba92ad706d))
- **kmd:** :sparkles: implement `ListMultiSigResponse` ([550a0ed](https://github.com/CareBoo/unity-algorand-sdk/commit/550a0edb96b79b14ad139d7fc72828b59184b2c4))
- **kmd:** :sparkles: implement `ListWalletsRequest` ([b3b5023](https://github.com/CareBoo/unity-algorand-sdk/commit/b3b5023fd1a4374f99fe787c31648ba87502c598))
- **kmd:** :sparkles: implement `ReleaseWalletHandleTokenRequest` ([ff024de](https://github.com/CareBoo/unity-algorand-sdk/commit/ff024de546b8faddcecf248dd3ae4b2f2f8b8d04))
- **kmd:** :sparkles: implement `ReleaseWalletHandleTokenResponse` ([719c393](https://github.com/CareBoo/unity-algorand-sdk/commit/719c39331096dd0058cb1fb6a85abc8a4ca48b20))
- **kmd:** :sparkles: implement `RenameWalletRequest` ([c5a9f3b](https://github.com/CareBoo/unity-algorand-sdk/commit/c5a9f3b549b482223e9bde8fa0c7324849b29408))
- **kmd:** :sparkles: implement `RenameWalletResponse` ([6e5b55e](https://github.com/CareBoo/unity-algorand-sdk/commit/6e5b55ef118d6e94816c4e47b942c759158ee5ea))
- **kmd:** :sparkles: implement `RenewWalletHandleTokenRequest` ([0a0d589](https://github.com/CareBoo/unity-algorand-sdk/commit/0a0d589b48b63e51eb90e4ab142f58090ebac76c))
- **kmd:** :sparkles: implement `RenewWalletHandleTokenResponse` ([d85e5f5](https://github.com/CareBoo/unity-algorand-sdk/commit/d85e5f5e5dbbe0ad9fb1b0afdae48bd87e70959e))
- **kmd:** :sparkles: implement `SignMultiSigRequest` ([e1dfee9](https://github.com/CareBoo/unity-algorand-sdk/commit/e1dfee9f4e7b2fb51c32b9fa881b4c6ec70e979d))
- **kmd:** :sparkles: implement `SignMultiSigResponse` ([83ea09f](https://github.com/CareBoo/unity-algorand-sdk/commit/83ea09fb0da43e0fa99545ea667012a996e1b8a6))
- **kmd:** :sparkles: implement `SignProgramMultiSigRequest` ([063ed1d](https://github.com/CareBoo/unity-algorand-sdk/commit/063ed1d0ee411b1dd17ea5fb21da348bed78c3f1))
- **kmd:** :sparkles: implement `SignProgramMultiSigResponse` ([b5e5b5e](https://github.com/CareBoo/unity-algorand-sdk/commit/b5e5b5ebe9b81cac41264343de6e3dcde3765736))
- **kmd:** :sparkles: implement `SignProgramRequest` ([52ccb07](https://github.com/CareBoo/unity-algorand-sdk/commit/52ccb07a62a1986ba3344e5c87d537f46fab499c))
- **kmd:** :sparkles: implement `SignTransactionRequest` ([c542582](https://github.com/CareBoo/unity-algorand-sdk/commit/c54258248defcff86d9484c423e91603ab0c78ad))
- **kmd:** :sparkles: implement `SignTransactionResponse` ([d7080ab](https://github.com/CareBoo/unity-algorand-sdk/commit/d7080ab90dd16f4b4d51570540a62ab6322d5256))
- **kmd:** :sparkles: implement `VersionsRequest` ([f70f0fd](https://github.com/CareBoo/unity-algorand-sdk/commit/f70f0fde67adf73fea202917c985f2372ea24d0c))
- **kmd:** :sparkles: implement `VersionsResponse` ([7c85ec2](https://github.com/CareBoo/unity-algorand-sdk/commit/7c85ec249642d8749c8fea7ec73f8e5dd28eb46c))
- **kmd:** :sparkles: implement `WalletInfoResponse` ([6b3edc7](https://github.com/CareBoo/unity-algorand-sdk/commit/6b3edc798c63d6518f29d9050bf81f953079876c))
- **kmd:** implement `SignProgramResponse` ([978a31d](https://github.com/CareBoo/unity-algorand-sdk/commit/978a31d0fa9e5af12872dc5efcba055405f96e5c))
- **msgpack:** :sparkles: implement `MessagePackReader.ReadRaw` ([c2dec22](https://github.com/CareBoo/unity-algorand-sdk/commit/c2dec22a8c520aa953964f449ebc84cac5b89dff))
- **msgpack:** :sparkles: implement `MessagePackWriter.WriteRaw` ([d5ae90e](https://github.com/CareBoo/unity-algorand-sdk/commit/d5ae90e320d855f0e8f3a5f59e87934f4390ff50))
- **networking:** :sparkles: add `ApiResponse` API to `ApiResponse<>` ([8556c53](https://github.com/CareBoo/unity-algorand-sdk/commit/8556c53616646d9b39d9a4a88488b66acb6ac421))
- **serialization:** :sparkles: add formatters for `PrivateKey`, `PublicKey`, and `Signature` ([2dedd67](https://github.com/CareBoo/unity-algorand-sdk/commit/2dedd67f4aa6676744fb48082343d68d96c4a805))
- **serialization:** :sparkles: add support for deserializing `byte[]` ([3ef497f](https://github.com/CareBoo/unity-algorand-sdk/commit/3ef497f3e1f6d31b23fc9bcef77c66f975e242b5))
- **serialization:** :sparkles: add support for generically serializing byte enums to strings ([424c8b4](https://github.com/CareBoo/unity-algorand-sdk/commit/424c8b49c2a567c015a356db3b12a4d4d035dd61))
- **serialization:** :sparkles: finish implementing `MessagePackReader.Integers` and `MessagePackWriter.Integers` ([37d8f20](https://github.com/CareBoo/unity-algorand-sdk/commit/37d8f20eba237ad28fcf84f83cdbc989b0168a86))
- **serialization:** :sparkles: finish implementing serializer/deserializer methods in `EnumFormatter` ([3af569f](https://github.com/CareBoo/unity-algorand-sdk/commit/3af569f180d26f5501a48d1eb43a00327bdd5921))
- **serialization:** :sparkles: implement `AlgoApiObject` ([74456b6](https://github.com/CareBoo/unity-algorand-sdk/commit/74456b60bb235eaf89b26d809adb2141b82d57f8))
- **serialization:** :sparkles: implement `Timestamp` ([cc1159b](https://github.com/CareBoo/unity-algorand-sdk/commit/cc1159b510aefc79c53fbe35154c5cf422e68f5f))
- **serialization:** :tada: update formatter cache ([029f841](https://github.com/CareBoo/unity-algorand-sdk/commit/029f8419b54810d582c3550b8a3b2e193946bf5a))
- **transaction:** :sparkles: add `GetSignature` and `ToRaw` extensions to `ITransaction` ([e4a3ad5](https://github.com/CareBoo/unity-algorand-sdk/commit/e4a3ad5e15e10155d9cb39dcaf61afc77989950e))
- **transaction:** :sparkles: add `RegisterAccountOnline` and `RegisterAccountOffline` APIs ([71d42c1](https://github.com/CareBoo/unity-algorand-sdk/commit/71d42c1d832212ddaa5ee872f8e332af570854ac))
- **transaction:** :sparkles: add `Transaction.AppCreate` and `Transaction.AppConfigure` APIs ([c64469a](https://github.com/CareBoo/unity-algorand-sdk/commit/c64469ac3e696c518d7d644d1e5b219bfd25d4b5))
- **transaction:** :sparkles: add `Transaction.AssetCreate` `Transaction.AssetConfigure` and `Transaction.AssetDelete` API ([259b37b](https://github.com/CareBoo/unity-algorand-sdk/commit/259b37b336a91a9d718343b4f189e4f9313a940c))
- **transaction:** :sparkles: add `Transaction.AssetFreeze` API ([160f837](https://github.com/CareBoo/unity-algorand-sdk/commit/160f83777f78b7a0fd53fd60b5241d766f4e6c17))
- **transaction:** :sparkles: add `Transaction.Payment` API ([88e918d](https://github.com/CareBoo/unity-algorand-sdk/commit/88e918dca1b6cc5376f138c5a010a50d8c504a21))
- **transaction:** :sparkles: implement address from application id ([9e3c883](https://github.com/CareBoo/unity-algorand-sdk/commit/9e3c8839abc58e11583e4974858cb794ac1cadc8))
- **transaction:** :sparkles: implement AssetClawback and AssetAccept ([c3578ec](https://github.com/CareBoo/unity-algorand-sdk/commit/c3578ec748160017c3da76dbcc35aee66515fc74)), closes [#24](https://github.com/CareBoo/unity-algorand-sdk/issues/24) [#25](https://github.com/CareBoo/unity-algorand-sdk/issues/25)
- **transaction:** :sparkles: implement atomic transfers ([21f7ee3](https://github.com/CareBoo/unity-algorand-sdk/commit/21f7ee34c14b0e9b99ba193f7b73215798dc5a76)), closes [#45](https://github.com/CareBoo/unity-algorand-sdk/issues/45)
- **transaction:** :sparkles: use `TransactionParams` in Transaction constructors ([a966320](https://github.com/CareBoo/unity-algorand-sdk/commit/a966320a0ba5f45225c77db23ef0742c43b73cfa))

### Reverts

- **demo:** :fire: remove bossroom assets... too big to be a sample ([e53192d](https://github.com/CareBoo/unity-algorand-sdk/commit/e53192daaba120f53d5c8f7312d2b199a8f4e564))

### BREAKING CHANGES

- **algod:** `AccountParticipation` no longer exists
- **transaction:** All transaction constructors have been changed to use `TransactionParams`
- **indexer:** Removed token from IndexerClient constructor
- **kmd:** changes `IKmdClient` API to use parameters instead of `Request` objs
- **indexer:** indexer queries are now given as optional arguments
- remove `Transaction.VerifySignature`, `SignedTransaction.Verify`
- signed transactions are now represented by `SignedTransaction` (non-generic)
- renamed `RawSignedTransaction` -> `SignedTransaction`
- `RawTransaction` renamed to `Transaction`
- AlgoApiKeyAttribute -> AlgoApiFieldAttribute
- **algod:** `Block` renamed to `BlockResponse`
- Replace Buffer with GetUnsafePtr
- Algo Serializer API has completely changed...
- Removes the `SendTransactionRaw` method from `AlgodClient`

# [1.0.0-exp.27](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.26...v1.0.0-exp.27) (2021-10-24)

### Bug Fixes

- **algod:** :bug: fix `AlgodClient.TealCompile` ([8f5a67a](https://github.com/CareBoo/unity-algorand-sdk/commit/8f5a67a694cd21496b6db7c1630324ddbfd13f1f)), closes [#20](https://github.com/CareBoo/unity-algorand-sdk/issues/20)
- **algod:** :bug: fix `AssetParams` serialization ([cbf9a69](https://github.com/CareBoo/unity-algorand-sdk/commit/cbf9a6981c52c565eb3837186c38765e5b8bea27))
- **algod:** :bug: fix vrfpubkey formatter lookup ([0bb0014](https://github.com/CareBoo/unity-algorand-sdk/commit/0bb001407b81abc288d271cda3c6d6b9c19c49df))
- **algod:** :bug: fix vrfpubkey formatting ([7d02443](https://github.com/CareBoo/unity-algorand-sdk/commit/7d024437ac9415449717ebf8a97017fcc10f9ee1))
- **algod:** :bug: replace `VrfPubKey` with `FixedString128Bytes` ([94b02b0](https://github.com/CareBoo/unity-algorand-sdk/commit/94b02b06f25d4b97e264f085cc96afbfdfb68862)), closes [#42](https://github.com/CareBoo/unity-algorand-sdk/issues/42)
- **kmd:** :art: fix `KmdClient` API incorrectly using optional args ([a53998b](https://github.com/CareBoo/unity-algorand-sdk/commit/a53998b00aa103a47ad1fea014c5f6627f8a3d34)), closes [#36](https://github.com/CareBoo/unity-algorand-sdk/issues/36)
- **serialization:** :bug: fix `TransactionId` serialization ([b7e07ca](https://github.com/CareBoo/unity-algorand-sdk/commit/b7e07caf5e8b8b5b0327c15c618a75263726dc18))
- **transaction:** :bug: fix `OnCompletion` having incorrect byte values ([514a378](https://github.com/CareBoo/unity-algorand-sdk/commit/514a378dce9c0669002960f6ef76d453a54fe0f0))
- **transaction:** :bug: fix incorrect constructor in `Transaction.ApplicationCall` ([98b5829](https://github.com/CareBoo/unity-algorand-sdk/commit/98b58295d7676d73a924a5d837b7f9b0dc8a62c9))

### Code Refactoring

- **algod:** :recycle: replace `AccountParticipation` with `Transaction.KeyRegistration.Params` ([3b624ee](https://github.com/CareBoo/unity-algorand-sdk/commit/3b624eec4974e28b0cc31b019d41f37dc60207ca))

### Features

- **transaction:** :sparkles: add `RegisterAccountOnline` and `RegisterAccountOffline` APIs ([71d42c1](https://github.com/CareBoo/unity-algorand-sdk/commit/71d42c1d832212ddaa5ee872f8e332af570854ac))
- **transaction:** :sparkles: add `Transaction.AppCreate` and `Transaction.AppConfigure` APIs ([c64469a](https://github.com/CareBoo/unity-algorand-sdk/commit/c64469ac3e696c518d7d644d1e5b219bfd25d4b5))
- **transaction:** :sparkles: add `Transaction.AssetCreate` `Transaction.AssetConfigure` and `Transaction.AssetDelete` API ([259b37b](https://github.com/CareBoo/unity-algorand-sdk/commit/259b37b336a91a9d718343b4f189e4f9313a940c))
- **transaction:** :sparkles: add `Transaction.AssetFreeze` API ([160f837](https://github.com/CareBoo/unity-algorand-sdk/commit/160f83777f78b7a0fd53fd60b5241d766f4e6c17))
- **transaction:** :sparkles: add `Transaction.Payment` API ([88e918d](https://github.com/CareBoo/unity-algorand-sdk/commit/88e918dca1b6cc5376f138c5a010a50d8c504a21))
- **transaction:** :sparkles: implement address from application id ([9e3c883](https://github.com/CareBoo/unity-algorand-sdk/commit/9e3c8839abc58e11583e4974858cb794ac1cadc8))
- **transaction:** :sparkles: implement AssetClawback and AssetAccept ([c3578ec](https://github.com/CareBoo/unity-algorand-sdk/commit/c3578ec748160017c3da76dbcc35aee66515fc74)), closes [#24](https://github.com/CareBoo/unity-algorand-sdk/issues/24) [#25](https://github.com/CareBoo/unity-algorand-sdk/issues/25)
- **transaction:** :sparkles: implement atomic transfers ([21f7ee3](https://github.com/CareBoo/unity-algorand-sdk/commit/21f7ee34c14b0e9b99ba193f7b73215798dc5a76)), closes [#45](https://github.com/CareBoo/unity-algorand-sdk/issues/45)
- **transaction:** :sparkles: use `TransactionParams` in Transaction constructors ([a966320](https://github.com/CareBoo/unity-algorand-sdk/commit/a966320a0ba5f45225c77db23ef0742c43b73cfa))

### BREAKING CHANGES

- **algod:** `AccountParticipation` no longer exists
- **transaction:** All transaction constructors have been changed to use `TransactionParams`

# [1.0.0-exp.27](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.26...v1.0.0-exp.27) (2021-10-23)

### Bug Fixes

- **algod:** :bug: fix `AlgodClient.TealCompile` ([8f5a67a](https://github.com/CareBoo/unity-algorand-sdk/commit/8f5a67a694cd21496b6db7c1630324ddbfd13f1f)), closes [#20](https://github.com/CareBoo/unity-algorand-sdk/issues/20)
- **algod:** :bug: fix `AssetParams` serialization ([cbf9a69](https://github.com/CareBoo/unity-algorand-sdk/commit/cbf9a6981c52c565eb3837186c38765e5b8bea27))
- **algod:** :bug: fix vrfpubkey formatter lookup ([0bb0014](https://github.com/CareBoo/unity-algorand-sdk/commit/0bb001407b81abc288d271cda3c6d6b9c19c49df))
- **algod:** :bug: fix vrfpubkey formatting ([7d02443](https://github.com/CareBoo/unity-algorand-sdk/commit/7d024437ac9415449717ebf8a97017fcc10f9ee1))
- **algod:** :bug: replace `VrfPubKey` with `FixedString128Bytes` ([94b02b0](https://github.com/CareBoo/unity-algorand-sdk/commit/94b02b06f25d4b97e264f085cc96afbfdfb68862)), closes [#42](https://github.com/CareBoo/unity-algorand-sdk/issues/42)
- **kmd:** :art: fix `KmdClient` API incorrectly using optional args ([a53998b](https://github.com/CareBoo/unity-algorand-sdk/commit/a53998b00aa103a47ad1fea014c5f6627f8a3d34)), closes [#36](https://github.com/CareBoo/unity-algorand-sdk/issues/36)
- **serialization:** :bug: fix `TransactionId` serialization ([b7e07ca](https://github.com/CareBoo/unity-algorand-sdk/commit/b7e07caf5e8b8b5b0327c15c618a75263726dc18))
- **transaction:** :bug: fix `OnCompletion` having incorrect byte values ([514a378](https://github.com/CareBoo/unity-algorand-sdk/commit/514a378dce9c0669002960f6ef76d453a54fe0f0))
- **transaction:** :bug: fix incorrect constructor in `Transaction.ApplicationCall` ([98b5829](https://github.com/CareBoo/unity-algorand-sdk/commit/98b58295d7676d73a924a5d837b7f9b0dc8a62c9))

### Code Refactoring

- **algod:** :recycle: replace `AccountParticipation` with `Transaction.KeyRegistration.Params` ([3b624ee](https://github.com/CareBoo/unity-algorand-sdk/commit/3b624eec4974e28b0cc31b019d41f37dc60207ca))

### Features

- **transaction:** :sparkles: add `RegisterAccountOnline` and `RegisterAccountOffline` APIs ([71d42c1](https://github.com/CareBoo/unity-algorand-sdk/commit/71d42c1d832212ddaa5ee872f8e332af570854ac))
- **transaction:** :sparkles: add `Transaction.AppCreate` and `Transaction.AppConfigure` APIs ([c64469a](https://github.com/CareBoo/unity-algorand-sdk/commit/c64469ac3e696c518d7d644d1e5b219bfd25d4b5))
- **transaction:** :sparkles: add `Transaction.AssetCreate` `Transaction.AssetConfigure` and `Transaction.AssetDelete` API ([259b37b](https://github.com/CareBoo/unity-algorand-sdk/commit/259b37b336a91a9d718343b4f189e4f9313a940c))
- **transaction:** :sparkles: add `Transaction.AssetFreeze` API ([160f837](https://github.com/CareBoo/unity-algorand-sdk/commit/160f83777f78b7a0fd53fd60b5241d766f4e6c17))
- **transaction:** :sparkles: add `Transaction.Payment` API ([88e918d](https://github.com/CareBoo/unity-algorand-sdk/commit/88e918dca1b6cc5376f138c5a010a50d8c504a21))
- **transaction:** :sparkles: implement address from application id ([9e3c883](https://github.com/CareBoo/unity-algorand-sdk/commit/9e3c8839abc58e11583e4974858cb794ac1cadc8))
- **transaction:** :sparkles: implement AssetClawback and AssetAccept ([c3578ec](https://github.com/CareBoo/unity-algorand-sdk/commit/c3578ec748160017c3da76dbcc35aee66515fc74)), closes [#24](https://github.com/CareBoo/unity-algorand-sdk/issues/24) [#25](https://github.com/CareBoo/unity-algorand-sdk/issues/25)
- **transaction:** :sparkles: implement atomic transfers ([21f7ee3](https://github.com/CareBoo/unity-algorand-sdk/commit/21f7ee34c14b0e9b99ba193f7b73215798dc5a76)), closes [#45](https://github.com/CareBoo/unity-algorand-sdk/issues/45)
- **transaction:** :sparkles: use `TransactionParams` in Transaction constructors ([a966320](https://github.com/CareBoo/unity-algorand-sdk/commit/a966320a0ba5f45225c77db23ef0742c43b73cfa))

### BREAKING CHANGES

- **algod:** `AccountParticipation` no longer exists
- **transaction:** All transaction constructors have been changed to use `TransactionParams`

# [1.0.0-exp.26](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.25...v1.0.0-exp.26) (2021-10-13)

### Bug Fixes

- **indexer:** :bug: add token back to indexer ([023cb05](https://github.com/CareBoo/unity-algorand-sdk/commit/023cb05f173242f2c3f234f8203fcf2c03ea5369))
- **indexer:** :bug: fix indexer requires token ([57f6114](https://github.com/CareBoo/unity-algorand-sdk/commit/57f61141f9c7763bb10a3a9deac07dcbc65ea6a9))
- **json:** :bug: fix `PrivateKey` JSON deserialization err ([cb327e0](https://github.com/CareBoo/unity-algorand-sdk/commit/cb327e0f38ef61e43f5c5e38c3090ab02b83693d))
- **json:** :bug: fix empty json objects missing begin object '{' char ([af9a2f1](https://github.com/CareBoo/unity-algorand-sdk/commit/af9a2f1cdc7b02a16e1ef052a1156ddee0d0a2a8))
- **kmd:** :bug: fix `GenerateKeyRequest` to use `wallet_handle_token` not password ([a86c25e](https://github.com/CareBoo/unity-algorand-sdk/commit/a86c25ea9b233dd755f234d64d36467415ce6ebb))
- **kmd:** :bug: fix `ImportKeyRequest` to use wallet_handle_token not password ([f020a03](https://github.com/CareBoo/unity-algorand-sdk/commit/f020a03fb19c9cf89452066d6dbd3cf278eb9f7f))
- **kmd:** :bug: fix `KmdClient.SignTransaction` returning a signed transaction message ([f0a354c](https://github.com/CareBoo/unity-algorand-sdk/commit/f0a354caeafd05ae7a984451582e8d53b82bbd6b))
- **kmd:** :bug: fix `WalletHandle` not added to formatter cache ([4c30939](https://github.com/CareBoo/unity-algorand-sdk/commit/4c309395b3f7076aa4db1e6ebfb552d59fad57b0))
- **kmd:** :bug: fix multisig ([fc7bcd6](https://github.com/CareBoo/unity-algorand-sdk/commit/fc7bcd6f647c399b3edebb52ccd65fccf715393e))

### Code Refactoring

- **kmd:** :recycle: replace `Request` with explicit method params ([030a0a9](https://github.com/CareBoo/unity-algorand-sdk/commit/030a0a9486f6270e6e867ad1efbad73b90e0ecfb))

### Features

- **json:** :sparkles: implement `JsonReader.Skip` ([0d6e6ab](https://github.com/CareBoo/unity-algorand-sdk/commit/0d6e6ab4c3b75e96c5e18b105019acb9c50180ac))
- **serialization:** :sparkles: add support for deserializing `byte[]` ([3ef497f](https://github.com/CareBoo/unity-algorand-sdk/commit/3ef497f3e1f6d31b23fc9bcef77c66f975e242b5))
- **transaction:** :sparkles: add `GetSignature` and `ToRaw` extensions to `ITransaction` ([e4a3ad5](https://github.com/CareBoo/unity-algorand-sdk/commit/e4a3ad5e15e10155d9cb39dcaf61afc77989950e))

### BREAKING CHANGES

- **indexer:** Removed token from IndexerClient constructor
- **kmd:** changes `IKmdClient` API to use parameters instead of `Request` objs

# [1.0.0-exp.25](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.24...v1.0.0-exp.25) (2021-10-08)

### Bug Fixes

- :bug: fix keyworded enum types formatting as numbers ([d686c13](https://github.com/CareBoo/unity-algorand-sdk/commit/d686c134dff5e0d657f974600c5948b86f506f0f))
- **algod:** :bug: fix pending transactions not returning messagepack ([dca09f8](https://github.com/CareBoo/unity-algorand-sdk/commit/dca09f8d6c2e1781097f3386355852b7179f01d9))
- **indexer:** :bug: add missing fields to `Account` model ([5732a1d](https://github.com/CareBoo/unity-algorand-sdk/commit/5732a1d7b6a57d300e8e8b4662696e180c0c2ecb))
- **indexer:** :bug: fix `HealthCheck` missing serialization logic ([f5165c6](https://github.com/CareBoo/unity-algorand-sdk/commit/f5165c681387450a791f191a2ae88a060741a9de))
- **indexer:** :fire: fix huge issue with indexer where query parameters were in body ([7bcca20](https://github.com/CareBoo/unity-algorand-sdk/commit/7bcca2022b8c1238cd35f225fccef80ada562143))
- **serialization:** :bug: fix `Block` not having a formatter ([dec6176](https://github.com/CareBoo/unity-algorand-sdk/commit/dec61762ddfba5c7f43223175c8c63f2d0b3609e))
- **serialization:** :bug: fix `BlockResponse` Serialization ([1252a16](https://github.com/CareBoo/unity-algorand-sdk/commit/1252a1634d6bb2fa00f78c1628ce7dbf33da6a56))
- **serialization:** :bug: fix `JsonWriter` not writing to `NativeText` ([2b61659](https://github.com/CareBoo/unity-algorand-sdk/commit/2b61659ea6801947f7b31069ecade0d6cb5f1e09))
- **serialization:** :bug: fix discrepancy between `TransactionId` and `TransactinIdResponse` ([d0c0370](https://github.com/CareBoo/unity-algorand-sdk/commit/d0c0370c0578e6d2296d0667557148e711c67174))
- **serialization:** :bug: fix issue where fixed strings were incorrectly being added to the queryparams ([a3e6fd9](https://github.com/CareBoo/unity-algorand-sdk/commit/a3e6fd9dbaf62d6e4bb05432568c0de36e3abb35))
- **serialization:** :bug: fix missing `AlgoApiObject` on all Transaction Params ([a11015f](https://github.com/CareBoo/unity-algorand-sdk/commit/a11015ff3da75db5ab5d6f7c809a89f50fe1142b))

### Features

- **networking:** :sparkles: add `ApiResponse` API to `ApiResponse<>` ([8556c53](https://github.com/CareBoo/unity-algorand-sdk/commit/8556c53616646d9b39d9a4a88488b66acb6ac421))
- **serialization:** :sparkles: add support for generically serializing byte enums to strings ([424c8b4](https://github.com/CareBoo/unity-algorand-sdk/commit/424c8b49c2a567c015a356db3b12a4d4d035dd61))
- **serialization:** :sparkles: finish implementing `MessagePackReader.Integers` and `MessagePackWriter.Integers` ([37d8f20](https://github.com/CareBoo/unity-algorand-sdk/commit/37d8f20eba237ad28fcf84f83cdbc989b0168a86))
- **serialization:** :sparkles: finish implementing serializer/deserializer methods in `EnumFormatter` ([3af569f](https://github.com/CareBoo/unity-algorand-sdk/commit/3af569f180d26f5501a48d1eb43a00327bdd5921))

### Reverts

- **demo:** :fire: remove bossroom assets... too big to be a sample ([e53192d](https://github.com/CareBoo/unity-algorand-sdk/commit/e53192daaba120f53d5c8f7312d2b199a8f4e564))

### BREAKING CHANGES

- **indexer:** indexer queries are now given as optional arguments

# [1.0.0-exp.24](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.23...v1.0.0-exp.24) (2021-10-05)

### Bug Fixes

- :bug: fix signatures ([2fb4614](https://github.com/CareBoo/unity-algorand-sdk/commit/2fb461459ac58266c1078bdb16607d65a25d8553))

### Features

- **kmd:** :sparkles: add `Error` to `ErrorMessage` which is used for kmd ([3c37a94](https://github.com/CareBoo/unity-algorand-sdk/commit/3c37a947ed4ff34b2b008562e8a40e6eaa142078))
- **kmd:** :sparkles: implement `APIV1DELETEKeyResponse` ([81f6c1e](https://github.com/CareBoo/unity-algorand-sdk/commit/81f6c1e14cf23ca1bca6a6e22ee76a3196a26f6f))
- **kmd:** :sparkles: implement `APIV1DELETEMultisigResponse` ([4c1d6db](https://github.com/CareBoo/unity-algorand-sdk/commit/4c1d6db3848c387d244bbb624030cd0fb2614dec))
- **kmd:** :sparkles: implement `APIV1GETWalletsResponse` ([9025e9a](https://github.com/CareBoo/unity-algorand-sdk/commit/9025e9a2dbf20fa2ed951d5ba602017901257984))
- **kmd:** :sparkles: implement `APIV1Wallet` ([e514183](https://github.com/CareBoo/unity-algorand-sdk/commit/e5141835d7db31cb7d4274b0e4c80976ce40291c))
- **kmd:** :sparkles: implement `APIV1WalletHandle` ([8bae4b4](https://github.com/CareBoo/unity-algorand-sdk/commit/8bae4b4e3385ef981a9375a791cdd3cb5e867578))
- **kmd:** :sparkles: implement `CreateWalletRequest` ([63882eb](https://github.com/CareBoo/unity-algorand-sdk/commit/63882eb7f363f34e510629c41276b6deeeadc718))
- **kmd:** :sparkles: implement `CreateWalletRequest` api fields ([f32094f](https://github.com/CareBoo/unity-algorand-sdk/commit/f32094f9235dd2c09e32935cea80e747bd0af597))
- **kmd:** :sparkles: implement `CreateWalletResponse` ([2579231](https://github.com/CareBoo/unity-algorand-sdk/commit/2579231460707e0824699c897deb136e29e2755e))
- **kmd:** :sparkles: implement `DeleteKeyRequest` ([5c89319](https://github.com/CareBoo/unity-algorand-sdk/commit/5c8931958bf638375843e8b57ef484dae713d936))
- **kmd:** :sparkles: implement `DeleteMultiSigRequest` ([15afda0](https://github.com/CareBoo/unity-algorand-sdk/commit/15afda09b50ebf543a05b95c5970cc61f6e68d7d))
- **kmd:** :sparkles: implement `ExportKeyRequest` ([a8b1131](https://github.com/CareBoo/unity-algorand-sdk/commit/a8b11314d037e7e8ece0273dd82b27f511433582))
- **kmd:** :sparkles: implement `ExportKeyResponse.Equals` ([010e656](https://github.com/CareBoo/unity-algorand-sdk/commit/010e656a3ddd7a9abea101a86828ad6c7b635c45))
- **kmd:** :sparkles: implement `ExportKeyResponse` ([db01766](https://github.com/CareBoo/unity-algorand-sdk/commit/db01766b0ad407e9088a1f2d1ba8487da5500b31))
- **kmd:** :sparkles: implement `ExportMasterKeyRequest` ([2a8ed81](https://github.com/CareBoo/unity-algorand-sdk/commit/2a8ed81b3b0577329dc862cea9d4de7a7c7b59fb))
- **kmd:** :sparkles: implement `ExportMasterKeyResponse` ([2843847](https://github.com/CareBoo/unity-algorand-sdk/commit/2843847d11145f3ee143d6f1a072fd59999e32f3))
- **kmd:** :sparkles: implement `ExportMultiSigRequest` ([e67c925](https://github.com/CareBoo/unity-algorand-sdk/commit/e67c925bce69227c730971840ec07984cb14711b))
- **kmd:** :sparkles: implement `ExportMultiSigResponse` ([bac0edb](https://github.com/CareBoo/unity-algorand-sdk/commit/bac0edb70256ceb82916eac7c6e29ef2f52aaf69))
- **kmd:** :sparkles: implement `GenerateKeyResponse` ([1d6795f](https://github.com/CareBoo/unity-algorand-sdk/commit/1d6795ffdfa76e0c20f2ec56ba4dea36ac7303aa))
- **kmd:** :sparkles: implement `GenerateMasterKeyRequest` ([6503dd8](https://github.com/CareBoo/unity-algorand-sdk/commit/6503dd8ef729d2a5460bcded8fad46efea99e77a))
- **kmd:** :sparkles: implement `ImportKeyRequest` ([521f7c1](https://github.com/CareBoo/unity-algorand-sdk/commit/521f7c1af4183cfa00d4f6ad2e4e6d7ac81dbfde))
- **kmd:** :sparkles: implement `ImportKeyResponse` ([83798a6](https://github.com/CareBoo/unity-algorand-sdk/commit/83798a65359906741b908e752d83f73ae0273613))
- **kmd:** :sparkles: implement `ImportMultiSigRequest` ([5eb5d42](https://github.com/CareBoo/unity-algorand-sdk/commit/5eb5d42d7d67919d9b7357708e5d3a68861877d5))
- **kmd:** :sparkles: implement `ImportMultiSigResponse` ([001f3ea](https://github.com/CareBoo/unity-algorand-sdk/commit/001f3eaffcf58aa5050cb8c5632dfff300adf69d))
- **kmd:** :sparkles: implement `InitWalletHandleTokenRequest` ([36dd459](https://github.com/CareBoo/unity-algorand-sdk/commit/36dd459e9923e3539ef291e4fdf0f8ea4c3afc09))
- **kmd:** :sparkles: implement `InitWalletHandleTokenResponse` ([26db31b](https://github.com/CareBoo/unity-algorand-sdk/commit/26db31bea84cb50e16d0194e8bf7b147469d1971))
- **kmd:** :sparkles: implement `KmdClient` ([7433060](https://github.com/CareBoo/unity-algorand-sdk/commit/743306013c6d5d7473990b9c8ad4f5055b036ba0)), closes [#13](https://github.com/CareBoo/unity-algorand-sdk/issues/13)
- **kmd:** :sparkles: implement `ListKeysRequest` ([b55fe67](https://github.com/CareBoo/unity-algorand-sdk/commit/b55fe67f5dd3a3b5316bef1dd32ecd421081214a))
- **kmd:** :sparkles: implement `ListKeysResponse` ([0096bd5](https://github.com/CareBoo/unity-algorand-sdk/commit/0096bd59eeb9ed56e65e1abb5cdda262c63979dc))
- **kmd:** :sparkles: implement `ListMultiSigRequest` ([ce667d1](https://github.com/CareBoo/unity-algorand-sdk/commit/ce667d10e43c7cb55e2b6b247760bfba92ad706d))
- **kmd:** :sparkles: implement `ListMultiSigResponse` ([550a0ed](https://github.com/CareBoo/unity-algorand-sdk/commit/550a0edb96b79b14ad139d7fc72828b59184b2c4))
- **kmd:** :sparkles: implement `ListWalletsRequest` ([b3b5023](https://github.com/CareBoo/unity-algorand-sdk/commit/b3b5023fd1a4374f99fe787c31648ba87502c598))
- **kmd:** :sparkles: implement `ReleaseWalletHandleTokenRequest` ([ff024de](https://github.com/CareBoo/unity-algorand-sdk/commit/ff024de546b8faddcecf248dd3ae4b2f2f8b8d04))
- **kmd:** :sparkles: implement `ReleaseWalletHandleTokenResponse` ([719c393](https://github.com/CareBoo/unity-algorand-sdk/commit/719c39331096dd0058cb1fb6a85abc8a4ca48b20))
- **kmd:** :sparkles: implement `RenameWalletRequest` ([c5a9f3b](https://github.com/CareBoo/unity-algorand-sdk/commit/c5a9f3b549b482223e9bde8fa0c7324849b29408))
- **kmd:** :sparkles: implement `RenameWalletResponse` ([6e5b55e](https://github.com/CareBoo/unity-algorand-sdk/commit/6e5b55ef118d6e94816c4e47b942c759158ee5ea))
- **kmd:** :sparkles: implement `RenewWalletHandleTokenRequest` ([0a0d589](https://github.com/CareBoo/unity-algorand-sdk/commit/0a0d589b48b63e51eb90e4ab142f58090ebac76c))
- **kmd:** :sparkles: implement `RenewWalletHandleTokenResponse` ([d85e5f5](https://github.com/CareBoo/unity-algorand-sdk/commit/d85e5f5e5dbbe0ad9fb1b0afdae48bd87e70959e))
- **kmd:** :sparkles: implement `SignMultiSigRequest` ([e1dfee9](https://github.com/CareBoo/unity-algorand-sdk/commit/e1dfee9f4e7b2fb51c32b9fa881b4c6ec70e979d))
- **kmd:** :sparkles: implement `SignMultiSigResponse` ([83ea09f](https://github.com/CareBoo/unity-algorand-sdk/commit/83ea09fb0da43e0fa99545ea667012a996e1b8a6))
- **kmd:** :sparkles: implement `SignProgramMultiSigRequest` ([063ed1d](https://github.com/CareBoo/unity-algorand-sdk/commit/063ed1d0ee411b1dd17ea5fb21da348bed78c3f1))
- **kmd:** :sparkles: implement `SignProgramMultiSigResponse` ([b5e5b5e](https://github.com/CareBoo/unity-algorand-sdk/commit/b5e5b5ebe9b81cac41264343de6e3dcde3765736))
- **kmd:** :sparkles: implement `SignProgramRequest` ([52ccb07](https://github.com/CareBoo/unity-algorand-sdk/commit/52ccb07a62a1986ba3344e5c87d537f46fab499c))
- **kmd:** :sparkles: implement `SignTransactionRequest` ([c542582](https://github.com/CareBoo/unity-algorand-sdk/commit/c54258248defcff86d9484c423e91603ab0c78ad))
- **kmd:** :sparkles: implement `SignTransactionResponse` ([d7080ab](https://github.com/CareBoo/unity-algorand-sdk/commit/d7080ab90dd16f4b4d51570540a62ab6322d5256))
- **kmd:** :sparkles: implement `VersionsRequest` ([f70f0fd](https://github.com/CareBoo/unity-algorand-sdk/commit/f70f0fde67adf73fea202917c985f2372ea24d0c))
- **kmd:** :sparkles: implement `VersionsResponse` ([7c85ec2](https://github.com/CareBoo/unity-algorand-sdk/commit/7c85ec249642d8749c8fea7ec73f8e5dd28eb46c))
- **kmd:** :sparkles: implement `WalletInfoResponse` ([6b3edc7](https://github.com/CareBoo/unity-algorand-sdk/commit/6b3edc798c63d6518f29d9050bf81f953079876c))
- **kmd:** implement `SignProgramResponse` ([978a31d](https://github.com/CareBoo/unity-algorand-sdk/commit/978a31d0fa9e5af12872dc5efcba055405f96e5c))
- **serialization:** :sparkles: add formatters for `PrivateKey`, `PublicKey`, and `Signature` ([2dedd67](https://github.com/CareBoo/unity-algorand-sdk/commit/2dedd67f4aa6676744fb48082343d68d96c4a805))
- **serialization:** :tada: update formatter cache ([029f841](https://github.com/CareBoo/unity-algorand-sdk/commit/029f8419b54810d582c3550b8a3b2e193946bf5a))

# [1.0.0-exp.23](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.22...v1.0.0-exp.23) (2021-10-03)

### Bug Fixes

- :bug: fix `RawTransaction.Equals` ([f6bd889](https://github.com/CareBoo/unity-algorand-sdk/commit/f6bd889c737f970c835d3273f7c28b002794ed3f))
- **indexer:** :bug: fix transaction formatters missing valid msgpack fields ([bf917ac](https://github.com/CareBoo/unity-algorand-sdk/commit/bf917ac30f964b9102c293d00a60d6f0072e75bd))
- **indexer:** :sparkles: fix `TealValue` msgpack fieldnames ([2a6ef27](https://github.com/CareBoo/unity-algorand-sdk/commit/2a6ef27e4f465c73a3cb744eae12267929c06f07))
- **serialization:** :bug: fix `SignedTransaction` serialization ([f4bde8d](https://github.com/CareBoo/unity-algorand-sdk/commit/f4bde8d4fd65e6146df35a6ce0890ed4417a235a))

### Code Refactoring

- :fire: remove some verify methods in the transaction ([70a8308](https://github.com/CareBoo/unity-algorand-sdk/commit/70a830830d3778a23d32fd9985d5d424972dd99f))
- :recycle: convert all `SignedTransaction<>` to a single `SignedTransaction` ([26fa65a](https://github.com/CareBoo/unity-algorand-sdk/commit/26fa65a53fa4cb2f4e8d81aa9ec2adc537d09b01))
- :recycle: rename `RawSignedTransaction` -> `SignedTransaction` ([059c6a3](https://github.com/CareBoo/unity-algorand-sdk/commit/059c6a369bd3f5e6d2b65965927cffd8bce65a49))
- :recycle: rename `RawTransaction` -> `Transaction` ([bade09e](https://github.com/CareBoo/unity-algorand-sdk/commit/bade09e7edc7df1d6dcbeb932f38fe7c199b21bb))
- :recycle: rename AlgoApiKeyAttribute -> AlgoApiFieldAttribute ([1b37028](https://github.com/CareBoo/unity-algorand-sdk/commit/1b370289ba2ffe8457f35c998aed6efcfe01931c))
- **algod:** :recycle: rename `Block` -> `BlockResponse` ([eca9fb6](https://github.com/CareBoo/unity-algorand-sdk/commit/eca9fb6c2be3a99272067d308a089148a1680427))

### Features

- :sparkles: add readOnly support ([6070565](https://github.com/CareBoo/unity-algorand-sdk/commit/6070565057130a23b52575126774a6d1fa58718a))
- **indexer:** :recycle: rename `ApplicationStateSchema` -> `StateSchema` ([7b02f70](https://github.com/CareBoo/unity-algorand-sdk/commit/7b02f70dbac6c3a67a0168107103a43be0a262ad))
- **indexer:** :sparkles: add `IIndexerClient` ([093153e](https://github.com/CareBoo/unity-algorand-sdk/commit/093153e104a09e4d9f3cafe27ea494d44fe35840))
- **indexer:** :sparkles: implement `AccountQuery` ([bda5301](https://github.com/CareBoo/unity-algorand-sdk/commit/bda5301687bf0a90d51860cd96074118639cc722))
- **indexer:** :sparkles: implement `AccountResponse` ([09d7fb9](https://github.com/CareBoo/unity-algorand-sdk/commit/09d7fb9b53afba88181a98ee66a4f95633ae7257))
- **indexer:** :sparkles: implement `AccountsQuery` ([e7d56e2](https://github.com/CareBoo/unity-algorand-sdk/commit/e7d56e29b559af8288e334e4006fe44bbab496b6))
- **indexer:** :sparkles: implement `AccountsResponse` ([ea01260](https://github.com/CareBoo/unity-algorand-sdk/commit/ea01260b0cc63fb0ee873b3f62c0a243fe20da30))
- **indexer:** :sparkles: implement `AddressRole` ([3325b60](https://github.com/CareBoo/unity-algorand-sdk/commit/3325b60757eac9400e5bf227639c2946b8f36a2d))
- **indexer:** :sparkles: implement `Application` ([144d04d](https://github.com/CareBoo/unity-algorand-sdk/commit/144d04d940c2361b636e3cff21a6134b08515980))
- **indexer:** :sparkles: implement `ApplicationLocalState` ([9ad284f](https://github.com/CareBoo/unity-algorand-sdk/commit/9ad284f734a27fb2cfc61a203f9400f397f63d31))
- **indexer:** :sparkles: implement `ApplicationQuery` ([de02370](https://github.com/CareBoo/unity-algorand-sdk/commit/de02370084ddf28ebf5473fd9016d0f634537c24))
- **indexer:** :sparkles: implement `ApplicationResponse` ([fa34b20](https://github.com/CareBoo/unity-algorand-sdk/commit/fa34b201d734a2deae62731ce224841ea9c00895))
- **indexer:** :sparkles: implement `ApplicationsQuery` ([914ca8b](https://github.com/CareBoo/unity-algorand-sdk/commit/914ca8b4b483f25d91be6e59b953a0d3b3fb2426))
- **indexer:** :sparkles: implement `ApplicationsResponse` ([e342cdc](https://github.com/CareBoo/unity-algorand-sdk/commit/e342cdc68d34e2582b0965af801eed60569fbd5e))
- **indexer:** :sparkles: implement `Asset` ([161f361](https://github.com/CareBoo/unity-algorand-sdk/commit/161f3618265027bf37f025c90326b3b96df24e8f))
- **indexer:** :sparkles: implement `AssetHolding` ([203785a](https://github.com/CareBoo/unity-algorand-sdk/commit/203785a112052f38c796bb6d9a5cef3a884cdbac))
- **indexer:** :sparkles: implement `AssetParams` ([ac78bfe](https://github.com/CareBoo/unity-algorand-sdk/commit/ac78bfedb61875fabb480e602eb9866f37bdd926))
- **indexer:** :sparkles: implement `AssetQuery` ([c8eda94](https://github.com/CareBoo/unity-algorand-sdk/commit/c8eda9423f316cc766f02e88ce6a0dea5805bde5))
- **indexer:** :sparkles: implement `AssetResponse` ([cd8e48f](https://github.com/CareBoo/unity-algorand-sdk/commit/cd8e48ff76f0959f848fc1dc5eb4f6cede1a6afd))
- **indexer:** :sparkles: implement `AssetsResponse` ([ade022d](https://github.com/CareBoo/unity-algorand-sdk/commit/ade022d73f1d0557b2259c676a5b3fd1493b8fb5))
- **indexer:** :sparkles: implement `BalancesQuery` ([b8f167e](https://github.com/CareBoo/unity-algorand-sdk/commit/b8f167eeea4dceb2493accf20dac409a0903950b))
- **indexer:** :sparkles: implement `BalancesResponse` ([c7ae299](https://github.com/CareBoo/unity-algorand-sdk/commit/c7ae29964bb26898f5d86519167eacd247265017))
- **indexer:** :sparkles: implement `Block` ([2533f24](https://github.com/CareBoo/unity-algorand-sdk/commit/2533f240584fe2022c4a2d64d90ebf2f9555da59))
- **indexer:** :sparkles: implement `BlockRewards` ([bb49cb0](https://github.com/CareBoo/unity-algorand-sdk/commit/bb49cb05bdade80cb283d9624eb751d9fd9bd112))
- **indexer:** :sparkles: implement `BlockUpgradeStatus` ([e9a0423](https://github.com/CareBoo/unity-algorand-sdk/commit/e9a0423125b1e2f2d5c2a0794cd9c705f84667c4))
- **indexer:** :sparkles: implement `BlockUpgradeVote` ([de5e72e](https://github.com/CareBoo/unity-algorand-sdk/commit/de5e72e097ee4abff6db3d0fe6bf1b5ba0067c6a))
- **indexer:** :sparkles: implement `ErrorResponse` msgpack fields ([358e6a6](https://github.com/CareBoo/unity-algorand-sdk/commit/358e6a6475f716e0df55ed75ee8f0173c885d1c1))
- **indexer:** :sparkles: implement `EvalDelta` msgpack fields ([45b2df2](https://github.com/CareBoo/unity-algorand-sdk/commit/45b2df29af0e15759fafab566ca52b320f5e3adc))
- **indexer:** :sparkles: implement `EvalDeltaKeyValue` msgpack fields ([bd3c230](https://github.com/CareBoo/unity-algorand-sdk/commit/bd3c2304f227c5e252f6b269d3c33468e9e796a3))
- **indexer:** :sparkles: implement `HealthCheck` ([a164577](https://github.com/CareBoo/unity-algorand-sdk/commit/a1645779a235d1e8c5201af61671292065f37cda))
- **indexer:** :sparkles: implement `IndexerClient.GetAccount` ([88ba11a](https://github.com/CareBoo/unity-algorand-sdk/commit/88ba11ac8d2dbf0d1aae786bb7f904a3c872fc53))
- **indexer:** :sparkles: implement `IndexerClient.GetAccounts` ([2e427f1](https://github.com/CareBoo/unity-algorand-sdk/commit/2e427f11f1a834d3d63c9f3eb3090f4330136342))
- **indexer:** :sparkles: implement `IndexerClient.GetHealth` ([0883ff0](https://github.com/CareBoo/unity-algorand-sdk/commit/0883ff0b17fb2123bf251ffbe0ada58f25cf4ecc))
- **indexer:** :sparkles: implement `IndexerClient` ([d7fd35a](https://github.com/CareBoo/unity-algorand-sdk/commit/d7fd35a195b216ffef6371ba099355b51a85f852))
- **indexer:** :sparkles: implement `LogicSig` json fields ([b22297c](https://github.com/CareBoo/unity-algorand-sdk/commit/b22297c25e71bbee318e779281904090d2cb79d7))
- **indexer:** :sparkles: implement `MiniAssetHolding` ([59941b8](https://github.com/CareBoo/unity-algorand-sdk/commit/59941b8002cfbcb37a24b1a80ecb96dc9cf9620f))
- **indexer:** :sparkles: implement `MultiSig.SubSignature` ([ef526ec](https://github.com/CareBoo/unity-algorand-sdk/commit/ef526ec427cb3e280ad7336ad6f65c748070a577))
- **indexer:** :sparkles: implement `MultiSig` ([df959a7](https://github.com/CareBoo/unity-algorand-sdk/commit/df959a75a1898605840402cc2805b5fd2b0cc4f9))
- **indexer:** :sparkles: implement `OnCompletion` ([04227b9](https://github.com/CareBoo/unity-algorand-sdk/commit/04227b95db85508f8f258963454a3095263dd80e))
- **indexer:** :sparkles: implement `TealKeyValue` msgPack fields ([e521553](https://github.com/CareBoo/unity-algorand-sdk/commit/e5215533bc5a0c25f71fbc38584b35d3a9b910b2))
- **indexer:** :sparkles: implement `Transaction.ApplicationCall` ([5a5e819](https://github.com/CareBoo/unity-algorand-sdk/commit/5a5e819ec408c44a0999f38db7bab3b4c16c2d0e))
- **indexer:** :sparkles: implement `Transaction.ApplicationCall` model fields ([69d64e8](https://github.com/CareBoo/unity-algorand-sdk/commit/69d64e8b14d0b6bf3da90ce4e782d13c9364745a))
- **indexer:** :sparkles: implement `Transaction.AssetConfiguration` model fields ([baf7c2e](https://github.com/CareBoo/unity-algorand-sdk/commit/baf7c2ea37cdc40c9e890c7c84e9ae51478df06c))
- **indexer:** :sparkles: implement `Transaction.AssetFreeze` model fields ([be45b90](https://github.com/CareBoo/unity-algorand-sdk/commit/be45b901e4478e404cd6aac77996de78470b75b1))
- **indexer:** :sparkles: implement `Transaction.AssetTransfer` model fields ([84aa15e](https://github.com/CareBoo/unity-algorand-sdk/commit/84aa15eef120f882177b4f57800036dccb91a6b5))
- **indexer:** :sparkles: implement `Transaction.KeyRegistration` ([69e004e](https://github.com/CareBoo/unity-algorand-sdk/commit/69e004eb56759117e9fc7bdba8adc67d4ac30ddd))
- **indexer:** :sparkles: implement `Transaction.KeyRegistration` model fields ([44029ca](https://github.com/CareBoo/unity-algorand-sdk/commit/44029ca8048cd9baf31d1a9aff9eea3b335ec93d))
- **indexer:** :sparkles: implement `TransactionResponse` ([430a1fa](https://github.com/CareBoo/unity-algorand-sdk/commit/430a1fa205e194766d22aa565a552769bebfc919))
- **indexer:** :sparkles: implement `TransactionsQuery` ([9ec7f4f](https://github.com/CareBoo/unity-algorand-sdk/commit/9ec7f4f5ec8e8df560ae6ea4a6b6bb548c203680))
- **indexer:** :sparkles: implement AssetsQuery ([e8d6e55](https://github.com/CareBoo/unity-algorand-sdk/commit/e8d6e550bc757c03c4f5bbff484d9e17e5283b09))
- **indexer:** :sparkles: implement TransactionsResponse ([e1a46f8](https://github.com/CareBoo/unity-algorand-sdk/commit/e1a46f877be174bf7605b1b8dac8f146cf850c62))
- **json:** :sparkles: implement `JsonWriter.WriteRaw` ([1643bad](https://github.com/CareBoo/unity-algorand-sdk/commit/1643badcf0069875a579da604ad0e2b5fb89d466))
- **json:** :sparkles: implement `ReadRaw` ([384abf8](https://github.com/CareBoo/unity-algorand-sdk/commit/384abf82e09f38724218d47675452c9f26595e21))
- **msgpack:** :sparkles: implement `MessagePackReader.ReadRaw` ([c2dec22](https://github.com/CareBoo/unity-algorand-sdk/commit/c2dec22a8c520aa953964f449ebc84cac5b89dff))
- **msgpack:** :sparkles: implement `MessagePackWriter.WriteRaw` ([d5ae90e](https://github.com/CareBoo/unity-algorand-sdk/commit/d5ae90e320d855f0e8f3a5f59e87934f4390ff50))
- **serialization:** :sparkles: implement `AlgoApiObject` ([74456b6](https://github.com/CareBoo/unity-algorand-sdk/commit/74456b60bb235eaf89b26d809adb2141b82d57f8))
- **serialization:** :sparkles: implement `Timestamp` ([cc1159b](https://github.com/CareBoo/unity-algorand-sdk/commit/cc1159b510aefc79c53fbe35154c5cf422e68f5f))

### BREAKING CHANGES

- remove `Transaction.VerifySignature`, `SignedTransaction.Verify`
- signed transactions are now represented by `SignedTransaction` (non-generic)
- renamed `RawSignedTransaction` -> `SignedTransaction`
- `RawTransaction` renamed to `Transaction`
- AlgoApiKeyAttribute -> AlgoApiFieldAttribute
- **algod:** `Block` renamed to `BlockResponse`

# [1.0.0-exp.22](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.21...v1.0.0-exp.22) (2021-09-28)

### Bug Fixes

- :bug: fix crash on ArrayComparer.Equals ([8ca1b06](https://github.com/CareBoo/unity-algorand-sdk/commit/8ca1b06daedcbdf97650f4b79bd72a134238bec0))
- :bug: fix issues with codegen and AOT compilation ([d2f9bdc](https://github.com/CareBoo/unity-algorand-sdk/commit/d2f9bdc8f8c77b8afb690162c3fafda81b5f8c48))

# [1.0.0-exp.21](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.20...v1.0.0-exp.21) (2021-09-28)

### Features

- :sparkles: update AlgoApiFormatterLookup ([cc112e5](https://github.com/CareBoo/unity-algorand-sdk/commit/cc112e55d4489e30afd2854757e482cf9d37c8ee))

# [1.0.0-exp.20](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.19...v1.0.0-exp.20) (2021-09-28)

### Features

- :sparkles: add FixedBytesFormatter ([941eb3f](https://github.com/CareBoo/unity-algorand-sdk/commit/941eb3f83b4b82d58431bc49b7f402e1ecfbccea))

### BREAKING CHANGES

- Replace Buffer with GetUnsafePtr

# [1.0.0-exp.19](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.18...v1.0.0-exp.19) (2021-09-25)

### Bug Fixes

- :bug: fix compile errs ([7a03ed9](https://github.com/CareBoo/unity-algorand-sdk/commit/7a03ed99a5cd7d3657df89c58a989ee0e33ced66))

# [1.0.0-exp.18](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.17...v1.0.0-exp.18) (2021-09-25)

### Features

- :sparkles: add LogicSig implementation ([51a21e0](https://github.com/CareBoo/unity-algorand-sdk/commit/51a21e02f98bb221ca2e14cbb8d1243f7b15b5ef))

# [1.0.0-exp.17](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.16...v1.0.0-exp.17) (2021-09-20)

### Features

- :sparkles: implement asset configuration transaction ([4f9ec58](https://github.com/CareBoo/unity-algorand-sdk/commit/4f9ec587932ed51da9bbbeaa1ddf3653a346c978)), closes [#27](https://github.com/CareBoo/unity-algorand-sdk/issues/27)
- :sparkles: implement AssetFreezeTransaction ([e3233bf](https://github.com/CareBoo/unity-algorand-sdk/commit/e3233bf9c8258eab91e8141c561d837f26780172)), closes [#26](https://github.com/CareBoo/unity-algorand-sdk/issues/26)
- :sparkles: implement AssetTransfer Transaction ([62a1841](https://github.com/CareBoo/unity-algorand-sdk/commit/62a1841332811753f48ac7fdccb1eec117607f3a)), closes [#23](https://github.com/CareBoo/unity-algorand-sdk/issues/23)

# [1.0.0-exp.16](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.15...v1.0.0-exp.16) (2021-09-19)

### Code Refactoring

- remove .NET 4.8 requirement ([9f55707](https://github.com/CareBoo/unity-algorand-sdk/commit/9f55707ba4f8a3d9b12fb7530b6ceec40c95219b)), closes [#19](https://github.com/CareBoo/unity-algorand-sdk/issues/19) [#9](https://github.com/CareBoo/unity-algorand-sdk/issues/9)

### BREAKING CHANGES

- Algo Serializer API has completely changed...

# [1.0.0-exp.15](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.14...v1.0.0-exp.15) (2021-09-03)

### Code Refactoring

- :fire: remove unnecessary `SendTransactionRaw` ([09b3bdc](https://github.com/CareBoo/unity-algorand-sdk/commit/09b3bdc0dfbeb2b47603b3cfc92be0478120bd09))

### BREAKING CHANGES

- Removes the `SendTransactionRaw` method from `AlgodClient`

# [1.0.0-exp.14](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.13...v1.0.0-exp.14) (2021-08-30)

### Features

- **algod:** implement Algod Client ([a7c9e90](https://github.com/CareBoo/unity-algorand-sdk/commit/a7c9e90e2f61a715488bfd8f0095c0206ae6e739)), closes [#10](https://github.com/CareBoo/unity-algorand-sdk/issues/10) [#16](https://github.com/CareBoo/unity-algorand-sdk/issues/16) [#18](https://github.com/CareBoo/unity-algorand-sdk/issues/18)

# [1.0.0-exp.13](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.12...v1.0.0-exp.13) (2021-08-07)

### Bug Fixes

- :white_check_mark: Fix CI tests on Github Actions can't find libsodium ([20c4ad5](https://github.com/CareBoo/unity-algorand-sdk/commit/20c4ad56edf57cef595c2f4078bba4e15d893602)), closes [#12](https://github.com/CareBoo/unity-algorand-sdk/issues/12)

# [1.0.0-exp.12](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.11...v1.0.0-exp.12) (2021-08-04)

### Features

- Add signed transaction support for payment transactions ([c07d370](https://github.com/CareBoo/unity-algorand-sdk/commit/c07d3705f70bcc33c245aa120c667a7dcc28d6e1))

# [1.0.0-exp.11](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.10...v1.0.0-exp.11) (2021-08-03)

### Features

- :sparkles: Add basic signed transaction support ([bb5900f](https://github.com/CareBoo/unity-algorand-sdk/commit/bb5900fa66bc9501c3d9da26e1e984ddd1fb2cc0))

# [1.0.0-exp.10](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.9...v1.0.0-exp.10) (2021-08-03)

### Features

- :sparkles: Add IEquatable, GetHashCode for RawTransaction ([885928e](https://github.com/CareBoo/unity-algorand-sdk/commit/885928e3c69dbc3742d11369d5e316853d6a8e8c))

# [1.0.0-exp.9](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.8...v1.0.0-exp.9) (2021-08-02)

### Features

- :sparkles: Add basic transaction support for message pack serialization and deserialization ([63243ed](https://github.com/CareBoo/unity-algorand-sdk/commit/63243ed492919522a752784756de2633d17a79fa))

# [1.0.0-exp.8](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.7...v1.0.0-exp.8) (2021-07-25)

### Features

- :sparkles: Added Transaction Header, Payment ([d3ebced](https://github.com/CareBoo/unity-algorand-sdk/commit/d3ebcedd3f86bdfabbb9f90a97e655fcadac1145))

# [1.0.0-exp.7](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.6...v1.0.0-exp.7) (2021-07-24)

### Features

- :sparkles: Add Account.Generate ([95233bf](https://github.com/CareBoo/unity-algorand-sdk/commit/95233bf877ee70d6e67f91baaf6f9e640b0a9141))

# [1.0.0-exp.6](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.5...v1.0.0-exp.6) (2021-07-24)

### Features

- :construction_worker: Add npmjs support ([56f60f0](https://github.com/CareBoo/unity-algorand-sdk/commit/56f60f0804c3cac377c78a5c47eea17785b29f9e))
- :heavy_plus_sign: Add libsodium ios, android, and windows ([00c9511](https://github.com/CareBoo/unity-algorand-sdk/commit/00c95113a152f01e737e56be6a31b3207aed57ff))
- :sparkles: Add Address struct ([d92b903](https://github.com/CareBoo/unity-algorand-sdk/commit/d92b90356cbf0070dd737a838eae417212a84a18))
- :sparkles: Add checksums ([2bc2189](https://github.com/CareBoo/unity-algorand-sdk/commit/2bc2189174e48de3324aa8ca4e3bbce22ca4e1c8))
- :sparkles: Add Sha512 interop ([86aabf5](https://github.com/CareBoo/unity-algorand-sdk/commit/86aabf5b0936cea36881cfaad1e1d87245755c92))
- :sparkles: Add start of Native Integration with Libsodium ([55e325f](https://github.com/CareBoo/unity-algorand-sdk/commit/55e325f4249603da679a66ece605f6ba8d5d762d))

# [1.0.0-exp.6](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.5...v1.0.0-exp.6) (2021-07-23)

### Features

- :construction_worker: Add npmjs support ([56f60f0](https://github.com/CareBoo/unity-algorand-sdk/commit/56f60f0804c3cac377c78a5c47eea17785b29f9e))
- :heavy_plus_sign: Add libsodium ios, android, and windows ([00c9511](https://github.com/CareBoo/unity-algorand-sdk/commit/00c95113a152f01e737e56be6a31b3207aed57ff))
- :sparkles: Add checksums ([2bc2189](https://github.com/CareBoo/unity-algorand-sdk/commit/2bc2189174e48de3324aa8ca4e3bbce22ca4e1c8))
- :sparkles: Add Sha512 interop ([86aabf5](https://github.com/CareBoo/unity-algorand-sdk/commit/86aabf5b0936cea36881cfaad1e1d87245755c92))
- :sparkles: Add start of Native Integration with Libsodium ([55e325f](https://github.com/CareBoo/unity-algorand-sdk/commit/55e325f4249603da679a66ece605f6ba8d5d762d))

# [1.0.0-exp.6](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.5...v1.0.0-exp.6) (2021-07-21)

### Features

- :construction_worker: Add npmjs support ([56f60f0](https://github.com/CareBoo/unity-algorand-sdk/commit/56f60f0804c3cac377c78a5c47eea17785b29f9e))
- :heavy_plus_sign: Add libsodium ios, android, and windows ([00c9511](https://github.com/CareBoo/unity-algorand-sdk/commit/00c95113a152f01e737e56be6a31b3207aed57ff))
- :sparkles: Add Sha512 interop ([86aabf5](https://github.com/CareBoo/unity-algorand-sdk/commit/86aabf5b0936cea36881cfaad1e1d87245755c92))
- :sparkles: Add start of Native Integration with Libsodium ([55e325f](https://github.com/CareBoo/unity-algorand-sdk/commit/55e325f4249603da679a66ece605f6ba8d5d762d))

# [1.0.0-exp.6](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.5...v1.0.0-exp.6) (2021-07-20)

### Features

- :construction_worker: Add npmjs support ([56f60f0](https://github.com/CareBoo/unity-algorand-sdk/commit/56f60f0804c3cac377c78a5c47eea17785b29f9e))
- :heavy_plus_sign: Add libsodium ios, android, and windows ([00c9511](https://github.com/CareBoo/unity-algorand-sdk/commit/00c95113a152f01e737e56be6a31b3207aed57ff))
- :sparkles: Add start of Native Integration with Libsodium ([55e325f](https://github.com/CareBoo/unity-algorand-sdk/commit/55e325f4249603da679a66ece605f6ba8d5d762d))

# [1.0.0-exp.6](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.5...v1.0.0-exp.6) (2021-07-20)

### Features

- :construction_worker: Add npmjs support ([56f60f0](https://github.com/CareBoo/unity-algorand-sdk/commit/56f60f0804c3cac377c78a5c47eea17785b29f9e))
- :heavy_plus_sign: Add libsodium ios, android, and windows ([00c9511](https://github.com/CareBoo/unity-algorand-sdk/commit/00c95113a152f01e737e56be6a31b3207aed57ff))
- :sparkles: Add start of Native Integration with Libsodium ([55e325f](https://github.com/CareBoo/unity-algorand-sdk/commit/55e325f4249603da679a66ece605f6ba8d5d762d))

# [1.0.0-exp.5](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.4...v1.0.0-exp.5) (2021-07-12)

### Bug Fixes

- :bug: Fix repository ([e765589](https://github.com/CareBoo/unity-algorand-sdk/commit/e765589b92849e23b7d080f335067cba1c1a8b62))

# [1.0.0-exp.4](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.3...v1.0.0-exp.4) (2021-07-12)

### Bug Fixes

- :bug: Fix npm release ([9e68078](https://github.com/CareBoo/unity-algorand-sdk/commit/9e68078b5f7f75a7670e7cf03dc1ee5137709bf1))

# [1.0.0-exp.3](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.2...v1.0.0-exp.3) (2021-07-12)

### Bug Fixes

- :triangular_flag_on_post: Update package registry ([a0800bc](https://github.com/CareBoo/unity-algorand-sdk/commit/a0800bc57c6fde758182075a90e2fbd96ce67be8))

# [1.0.0-exp.2](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.1...v1.0.0-exp.2) (2021-07-12)

### Features

- :sparkles: Add Mnemonic.FromString and Mnemonic.ToString ([d3a88c6](https://github.com/CareBoo/unity-algorand-sdk/commit/d3a88c6026ff3162a46f1e3d13ca54d103d43aaf))

# 1.0.0-exp.1 (2021-05-18)

### Features

- Add Mnemonic and Key datastructs ([4370f74](https://github.com/CareBoo/Algorand.SDK.Unity/commit/4370f74c4e5f5ed9977dc4954313bf57e7023547))
