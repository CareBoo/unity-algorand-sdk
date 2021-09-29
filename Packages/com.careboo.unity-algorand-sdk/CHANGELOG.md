# [1.0.0-exp.22](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.21...v1.0.0-exp.22) (2021-09-28)


### Bug Fixes

* :bug: fix crash on ArrayComparer.Equals ([8ca1b06](https://github.com/CareBoo/unity-algorand-sdk/commit/8ca1b06daedcbdf97650f4b79bd72a134238bec0))
* :bug: fix issues with codegen and AOT compilation ([d2f9bdc](https://github.com/CareBoo/unity-algorand-sdk/commit/d2f9bdc8f8c77b8afb690162c3fafda81b5f8c48))

# [1.0.0-exp.21](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.20...v1.0.0-exp.21) (2021-09-28)


### Features

* :sparkles: update AlgoApiFormatterLookup ([cc112e5](https://github.com/CareBoo/unity-algorand-sdk/commit/cc112e55d4489e30afd2854757e482cf9d37c8ee))

# [1.0.0-exp.20](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.19...v1.0.0-exp.20) (2021-09-28)


### Features

* :sparkles: add FixedBytesFormatter ([941eb3f](https://github.com/CareBoo/unity-algorand-sdk/commit/941eb3f83b4b82d58431bc49b7f402e1ecfbccea))


### BREAKING CHANGES

* Replace Buffer with GetUnsafePtr

# [1.0.0-exp.19](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.18...v1.0.0-exp.19) (2021-09-25)


### Bug Fixes

* :bug: fix compile errs ([7a03ed9](https://github.com/CareBoo/unity-algorand-sdk/commit/7a03ed99a5cd7d3657df89c58a989ee0e33ced66))

# [1.0.0-exp.18](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.17...v1.0.0-exp.18) (2021-09-25)


### Features

* :sparkles: add LogicSig implementation ([51a21e0](https://github.com/CareBoo/unity-algorand-sdk/commit/51a21e02f98bb221ca2e14cbb8d1243f7b15b5ef))

# [1.0.0-exp.17](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.16...v1.0.0-exp.17) (2021-09-20)


### Features

* :sparkles: implement asset configuration transaction ([4f9ec58](https://github.com/CareBoo/unity-algorand-sdk/commit/4f9ec587932ed51da9bbbeaa1ddf3653a346c978)), closes [#27](https://github.com/CareBoo/unity-algorand-sdk/issues/27)
* :sparkles: implement AssetFreezeTransaction ([e3233bf](https://github.com/CareBoo/unity-algorand-sdk/commit/e3233bf9c8258eab91e8141c561d837f26780172)), closes [#26](https://github.com/CareBoo/unity-algorand-sdk/issues/26)
* :sparkles: implement AssetTransfer Transaction ([62a1841](https://github.com/CareBoo/unity-algorand-sdk/commit/62a1841332811753f48ac7fdccb1eec117607f3a)), closes [#23](https://github.com/CareBoo/unity-algorand-sdk/issues/23)

# [1.0.0-exp.16](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.15...v1.0.0-exp.16) (2021-09-19)


### Code Refactoring

* remove .NET 4.8 requirement ([9f55707](https://github.com/CareBoo/unity-algorand-sdk/commit/9f55707ba4f8a3d9b12fb7530b6ceec40c95219b)), closes [#19](https://github.com/CareBoo/unity-algorand-sdk/issues/19) [#9](https://github.com/CareBoo/unity-algorand-sdk/issues/9)


### BREAKING CHANGES

* Algo Serializer API has completely changed...

# [1.0.0-exp.15](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.14...v1.0.0-exp.15) (2021-09-03)


### Code Refactoring

* :fire: remove unnecessary `SendTransactionRaw` ([09b3bdc](https://github.com/CareBoo/unity-algorand-sdk/commit/09b3bdc0dfbeb2b47603b3cfc92be0478120bd09))


### BREAKING CHANGES

* Removes the `SendTransactionRaw` method from `AlgodClient`

# [1.0.0-exp.14](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.13...v1.0.0-exp.14) (2021-08-30)


### Features

* **algod:** implement Algod Client ([a7c9e90](https://github.com/CareBoo/unity-algorand-sdk/commit/a7c9e90e2f61a715488bfd8f0095c0206ae6e739)), closes [#10](https://github.com/CareBoo/unity-algorand-sdk/issues/10) [#16](https://github.com/CareBoo/unity-algorand-sdk/issues/16) [#18](https://github.com/CareBoo/unity-algorand-sdk/issues/18)

# [1.0.0-exp.13](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.12...v1.0.0-exp.13) (2021-08-07)


### Bug Fixes

* :white_check_mark: Fix CI tests on Github Actions can't find libsodium ([20c4ad5](https://github.com/CareBoo/unity-algorand-sdk/commit/20c4ad56edf57cef595c2f4078bba4e15d893602)), closes [#12](https://github.com/CareBoo/unity-algorand-sdk/issues/12)

# [1.0.0-exp.12](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.11...v1.0.0-exp.12) (2021-08-04)


### Features

* Add signed transaction support for payment transactions ([c07d370](https://github.com/CareBoo/unity-algorand-sdk/commit/c07d3705f70bcc33c245aa120c667a7dcc28d6e1))

# [1.0.0-exp.11](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.10...v1.0.0-exp.11) (2021-08-03)


### Features

* :sparkles: Add basic signed transaction support ([bb5900f](https://github.com/CareBoo/unity-algorand-sdk/commit/bb5900fa66bc9501c3d9da26e1e984ddd1fb2cc0))

# [1.0.0-exp.10](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.9...v1.0.0-exp.10) (2021-08-03)


### Features

* :sparkles: Add IEquatable, GetHashCode for RawTransaction ([885928e](https://github.com/CareBoo/unity-algorand-sdk/commit/885928e3c69dbc3742d11369d5e316853d6a8e8c))

# [1.0.0-exp.9](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.8...v1.0.0-exp.9) (2021-08-02)


### Features

* :sparkles: Add basic transaction support for message pack serialization and deserialization ([63243ed](https://github.com/CareBoo/unity-algorand-sdk/commit/63243ed492919522a752784756de2633d17a79fa))

# [1.0.0-exp.8](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.7...v1.0.0-exp.8) (2021-07-25)


### Features

* :sparkles: Added Transaction Header, Payment ([d3ebced](https://github.com/CareBoo/unity-algorand-sdk/commit/d3ebcedd3f86bdfabbb9f90a97e655fcadac1145))

# [1.0.0-exp.7](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.6...v1.0.0-exp.7) (2021-07-24)


### Features

* :sparkles: Add Account.Generate ([95233bf](https://github.com/CareBoo/unity-algorand-sdk/commit/95233bf877ee70d6e67f91baaf6f9e640b0a9141))

# [1.0.0-exp.6](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.5...v1.0.0-exp.6) (2021-07-24)


### Features

* :construction_worker: Add npmjs support ([56f60f0](https://github.com/CareBoo/unity-algorand-sdk/commit/56f60f0804c3cac377c78a5c47eea17785b29f9e))
* :heavy_plus_sign: Add libsodium ios, android, and windows ([00c9511](https://github.com/CareBoo/unity-algorand-sdk/commit/00c95113a152f01e737e56be6a31b3207aed57ff))
* :sparkles: Add Address struct ([d92b903](https://github.com/CareBoo/unity-algorand-sdk/commit/d92b90356cbf0070dd737a838eae417212a84a18))
* :sparkles: Add checksums ([2bc2189](https://github.com/CareBoo/unity-algorand-sdk/commit/2bc2189174e48de3324aa8ca4e3bbce22ca4e1c8))
* :sparkles: Add Sha512 interop ([86aabf5](https://github.com/CareBoo/unity-algorand-sdk/commit/86aabf5b0936cea36881cfaad1e1d87245755c92))
* :sparkles: Add start of Native Integration with Libsodium ([55e325f](https://github.com/CareBoo/unity-algorand-sdk/commit/55e325f4249603da679a66ece605f6ba8d5d762d))

# [1.0.0-exp.6](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.5...v1.0.0-exp.6) (2021-07-23)


### Features

* :construction_worker: Add npmjs support ([56f60f0](https://github.com/CareBoo/unity-algorand-sdk/commit/56f60f0804c3cac377c78a5c47eea17785b29f9e))
* :heavy_plus_sign: Add libsodium ios, android, and windows ([00c9511](https://github.com/CareBoo/unity-algorand-sdk/commit/00c95113a152f01e737e56be6a31b3207aed57ff))
* :sparkles: Add checksums ([2bc2189](https://github.com/CareBoo/unity-algorand-sdk/commit/2bc2189174e48de3324aa8ca4e3bbce22ca4e1c8))
* :sparkles: Add Sha512 interop ([86aabf5](https://github.com/CareBoo/unity-algorand-sdk/commit/86aabf5b0936cea36881cfaad1e1d87245755c92))
* :sparkles: Add start of Native Integration with Libsodium ([55e325f](https://github.com/CareBoo/unity-algorand-sdk/commit/55e325f4249603da679a66ece605f6ba8d5d762d))

# [1.0.0-exp.6](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.5...v1.0.0-exp.6) (2021-07-21)


### Features

* :construction_worker: Add npmjs support ([56f60f0](https://github.com/CareBoo/unity-algorand-sdk/commit/56f60f0804c3cac377c78a5c47eea17785b29f9e))
* :heavy_plus_sign: Add libsodium ios, android, and windows ([00c9511](https://github.com/CareBoo/unity-algorand-sdk/commit/00c95113a152f01e737e56be6a31b3207aed57ff))
* :sparkles: Add Sha512 interop ([86aabf5](https://github.com/CareBoo/unity-algorand-sdk/commit/86aabf5b0936cea36881cfaad1e1d87245755c92))
* :sparkles: Add start of Native Integration with Libsodium ([55e325f](https://github.com/CareBoo/unity-algorand-sdk/commit/55e325f4249603da679a66ece605f6ba8d5d762d))

# [1.0.0-exp.6](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.5...v1.0.0-exp.6) (2021-07-20)


### Features

* :construction_worker: Add npmjs support ([56f60f0](https://github.com/CareBoo/unity-algorand-sdk/commit/56f60f0804c3cac377c78a5c47eea17785b29f9e))
* :heavy_plus_sign: Add libsodium ios, android, and windows ([00c9511](https://github.com/CareBoo/unity-algorand-sdk/commit/00c95113a152f01e737e56be6a31b3207aed57ff))
* :sparkles: Add start of Native Integration with Libsodium ([55e325f](https://github.com/CareBoo/unity-algorand-sdk/commit/55e325f4249603da679a66ece605f6ba8d5d762d))

# [1.0.0-exp.6](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.5...v1.0.0-exp.6) (2021-07-20)


### Features

* :construction_worker: Add npmjs support ([56f60f0](https://github.com/CareBoo/unity-algorand-sdk/commit/56f60f0804c3cac377c78a5c47eea17785b29f9e))
* :heavy_plus_sign: Add libsodium ios, android, and windows ([00c9511](https://github.com/CareBoo/unity-algorand-sdk/commit/00c95113a152f01e737e56be6a31b3207aed57ff))
* :sparkles: Add start of Native Integration with Libsodium ([55e325f](https://github.com/CareBoo/unity-algorand-sdk/commit/55e325f4249603da679a66ece605f6ba8d5d762d))

# [1.0.0-exp.5](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.4...v1.0.0-exp.5) (2021-07-12)


### Bug Fixes

* :bug: Fix repository ([e765589](https://github.com/CareBoo/unity-algorand-sdk/commit/e765589b92849e23b7d080f335067cba1c1a8b62))

# [1.0.0-exp.4](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.3...v1.0.0-exp.4) (2021-07-12)


### Bug Fixes

* :bug: Fix npm release ([9e68078](https://github.com/CareBoo/unity-algorand-sdk/commit/9e68078b5f7f75a7670e7cf03dc1ee5137709bf1))

# [1.0.0-exp.3](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.2...v1.0.0-exp.3) (2021-07-12)


### Bug Fixes

* :triangular_flag_on_post: Update package registry ([a0800bc](https://github.com/CareBoo/unity-algorand-sdk/commit/a0800bc57c6fde758182075a90e2fbd96ce67be8))

# [1.0.0-exp.2](https://github.com/CareBoo/unity-algorand-sdk/compare/v1.0.0-exp.1...v1.0.0-exp.2) (2021-07-12)


### Features

* :sparkles: Add Mnemonic.FromString and Mnemonic.ToString ([d3a88c6](https://github.com/CareBoo/unity-algorand-sdk/commit/d3a88c6026ff3162a46f1e3d13ca54d103d43aaf))

# 1.0.0-exp.1 (2021-05-18)


### Features

* Add Mnemonic and Key datastructs ([4370f74](https://github.com/CareBoo/Algorand.SDK.Unity/commit/4370f74c4e5f5ed9977dc4954313bf57e7023547))

# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [0.1.0] - 2020-08-18

### This is the first release of *\<My Package\>*.

*Short description of this release*
