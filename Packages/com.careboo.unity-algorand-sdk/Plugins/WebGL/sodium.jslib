mergeInto(LibraryManager.library, {
  sodium_malloc: function (size) {
    return Module._malloc(size);
  },

  sodium_free: function (handle) {
    Module._free(handle);
  },

  crypto_hash_sha512_256: function (output, input, inputLen) {
    var inputBytes = Module.HEAPU8.subarray(input, input + inputLen);
    var hashBytes = window.sha512.sha512_256.array(inputBytes);
    var outputBytes = Module.HEAPU8.subarray(output, output + 32);
    outputBytes.set(hashBytes);
  },

  randombytes_buf: function (output, size) {
    var randomBytes = window.nacl.randomBytes(size);
    var outputBytes = Module.HEAPU8.subarray(output, output + size);
    outputBytes.set(randomBytes);
  },

  crypto_sign_ed25519_seed_keypair: function (pk, sk, seed) {
    var seedBytes = Module.HEAPU8.subarray(seed, seed + window.nacl.sign.seedLength);
    var kp = window.nacl.sign.keyPair.fromSeed(seedBytes);
    var skBytes = Module.HEAPU8.subarray(sk, sk + window.nacl.sign.secretKeyLength);
    var pkBytes = Module.HEAPU8.subarray(pk, pk + window.nacl.sign.publicKeyLength);
    skBytes.set(kp.secretKey);
    pkBytes.set(kp.publicKey);
  },

  crypto_sign_ed25519_detached: function (signature, message, messageLength, sk) {
    var messageBytes = Module.HEAPU8.subarray(message, message + messageLength);
    var skBytes = Module.HEAPU8.subarray(sk, sk + window.nacl.sign.secretKeyLength);
    var signed = window.nacl.sign.detached(messageBytes, skBytes);
    var sigBytes = Module.HEAPU8.subarray(signature, signature + window.nacl.sign.signatureLength);
    sigBytes.set(signed);
  },

  crypto_sign_ed25519_verify_detached: function (signature, message, messageLength, pk) {
    var msgBytes = Module.HEAPU8.subarray(message, message + messageLength);
    var pkBytes = Module.HEAPU8.subarray(pk, pk + window.nacl.sign.publicKeyLength);
    var sigBytes = Module.HEAPU8.subarray(signature, signature + window.nacl.sign.signatureLength);
    return window.nacl.sign.detached.verify(msgBytes, sigBytes, pkBytes) ? 0 : 1;
  },
});
