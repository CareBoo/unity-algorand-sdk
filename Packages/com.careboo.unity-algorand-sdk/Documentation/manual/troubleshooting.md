# Troubleshooting

## Debugging Web Requests

Setting the `UNITY_ALGO_SDK_DEBUG` #define directives will turn on additional HTTP request logging:

```
completed request
	url: https://testnet-algorand.api.purestake.io/idx2/health
	uploadedData:
	downloadedData: {"data":{"migration-required":false,"read-only-mode":true},"db-available":true,"is-migrating":false,"message":"17756385","round":17756385}

	error:
	method: GET
	downloadHandler.error:

<...snip...>
```

If the data is in JSON or plaintext format, then it will show in the logs as the raw JSON text.
If the data is in Message Pack format, it will show as base-64 encoded byte string.

> [!Note]
> When debugging Message Pack, it can be useful to use a visualizer like [this one](https://msgpack.dbrgn.ch/).
