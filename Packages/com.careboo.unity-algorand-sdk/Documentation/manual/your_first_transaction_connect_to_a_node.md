Connect to a Node
-----------------

The easiest way to access a node in development is via the Algorand Sandbox.
Clone the repository from https://github.com/algorand/sandbox and make sure
you have [Docker Compose](https://docs.docker.com/compose/install/) installed. Then
`cd` into the cloned repository and start the sandbox with

```bash
> ./sandbox up dev
```

The `dev` option will configure the network so that transactions are finalized faster.

Once the network is up and running, you should be able to connect to the `algod` service.
Verify that the node is healthy and you can connect to it. Create a new `AlgodCheck` component
that creates an `AlgodClient` on `Start()` and makes a `GetHealth()` request.

```csharp
using AlgoSdk;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AlgodCheck : MonoBehaviour
{
    AlgodClient algod;

    public void Start()
    {
        algod = new AlgodClient(
            address: "http://localhost:4001",
            token: "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
        );
        CheckAlgodStatus().Forget();
    }

    public async UniTaskVoid CheckAlgodStatus()
    {
        var response = await algod.GetHealth();
        if (response.Error.IsError)
        {
            Debug.LogError(response.Error.Message);
        }
        else
        {
            Debug.Log("Connected to algod!");
        }
    }
}
```
Add the `AlgodCheck` component to a `GameObject` in a new scene, and hit **Play**. You should see the
`Connected to algod!` message in the editor console. If you cannot connect, or the node is not healthy,
then you should see an error message in the console.
