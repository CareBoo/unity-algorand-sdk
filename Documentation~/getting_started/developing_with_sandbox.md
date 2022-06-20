# Developing with Algorand Sandbox

The [Algorand Sandbox](https://github.com/algorand/sandbox) allows developers to run a local network of the Algorand Blockchain on their machine. This is the **easiest** fastest way to iterate and develop using the Algorand blockchain.

## Starting the Sandbox

You can follow the instructions to run the sandbox at its [GitHub Repository](https://github.com/algorand/sandbox). However, I've found the easiest way to setup the sandbox is to run it via `docker-compose`.

### Running via Docker Compose

1. [Install `docker`](https://docs.docker.com/get-docker/) which should come with an installation of `docker-compose`.
2. Add the following `docker-compose.yml` file to the your project.

   ```yaml
   version: "3"

   services:
   algod:
     image: makerxau/algorand-sandbox-dev:latest
     ports:
       - "4001:4001"
       - "4002:4002"
       - "9392:9392"

   indexer:
     image: makerxau/algorand-indexer-dev:latest
     ports:
       - "8980:8980"
     restart: unless-stopped
     environment:
     ALGOD_HOST: "algod"
     POSTGRES_HOST: "indexer-db"
     POSTGRES_PORT: "5432"
     POSTGRES_USER: algorand
     POSTGRES_PASSWORD: algorand
     POSTGRES_DB: indexer_db
     depends_on:
       - indexer-db
       - algod

   indexer-db:
     image: "postgres:13-alpine"
     container_name: "algorand-sandbox-postgres"
     ports:
       - "5433:5432"
     user: postgres
     environment:
     POSTGRES_USER: algorand
     POSTGRES_PASSWORD: algorand
     POSTGRES_DB: indexer_db
   ```

3. Change directory to the location of your `docker-compose.yml` then run the following command:

   ```sh
   > docker-compose up -d
   ```

4. Wait for services to come up, and you should now have access to the sandbox locally!

## Connecting

If you've setup the sandbox services using the above guides, you should have access to node services locally on your machine with the following configurations

| Service   | Address               | Token                                                                | Client Create Expression                                                                                       |
| --------- | --------------------- | -------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------- |
| `algod`   | http://localhost:4001 | `"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"` | `new AlgodClient("http://localhost:4001", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")` |
| `indexer` | http://localhost:8980 | `null`                                                               | `new IndexerClient("http://localhost:8980")`                                                                   |
| `kmd`     | http://localhost:4002 | `"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"` | `new KmdClient("http://localhost:4002", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")`   |

## Thanks

Thanks to [makerxau](https://hub.docker.com/u/makerxau) for uploading pre-built dev sandbox images.
