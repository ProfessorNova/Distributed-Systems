# Distributed Systems

This repository contains the code for the Distributed Systems course at Hochschule Esslingen.
It implements the required backend for the course project.

The API which is provided by the backend is defined in the `REST_API.yml` file which is located in the root directory of the repository.
The backend is implemented in DotNet Core 9.0 and will communicate with a PostgreSQL database.

## Setup the devcontainer

In the `.devcontainer` directory you can find the configuration for the devcontainer.
This allows you to run everything in GitHub Codespaces or locally in a Docker container.

## Project Structure

- The `.devcontainer` directory contains the configuration for the devcontainer.

- The `backend/ShoppingApi` directory contains the backend code for the project.

  - The `Controllers` directory contains the item controller for the API.
  - The `Data` directory contains the database context.
  - The `DTOs` directory contains the data transfer objects for the API.
  - The `Models` directory contains the item model.
  - The `Properties` directory contains the default launch settings for the backend.
  - The `Dockerfile` file is used to build the Docker image for the backend.
  - The `Program.cs` file is the entry point for the backend.
  - The `ShoppingApi.csproj` file is the project file for the backend where all dependencies are defined.

- The `.gitignore` file is used to ignore files and directories that should not be tracked by Git.

- The `Distributed-Systems.sln` file is the solution file for the project.

- The `docker-compose.yml` file is used to build the Docker image for the backend and the PostgreSQL database.

- The `README.md` file is this file.

- The `REST_API.yml` file contains the API definition for the backend.

## Running Docker Compose

The backend is expected to run with a PostgreSQL database.
Everything is configured in the `docker-compose.yml` file.
The ports and database name can be changed in the `docker-compose.yml` file.
The frontend is given by the course and is not part of this repository.
It will be pulled from Docker Hub when running the `docker-compose.yml` file.

By running the following command in the root directory of the repository, the frontend, backend, and database will be started:

```bash
docker compose up --build
```

The frontend will be available at `http://localhost:5000`.

The backend will be available at `http://localhost:3000`.

## Running Kubernetes

The Kubernetes configuration is located in the `k8s` directory.
The `k8s` directory contains the configuration for the backend and the PostgreSQL database.
The `k8s` directory contains the following files:
- `namespace.yaml`: This file creates the namespace for the backend and the PostgreSQL database.
- `postgresql-secret.yaml`: This file creates the secret for the PostgreSQL database.
- `postgresql-pvc.yaml`: This file creates the persistent volume claim for the PostgreSQL database.
- `postgresql-deployment.yaml`: This file creates the deployment for the PostgreSQL database.
- `postgresql-service.yaml`: This file creates the service for the PostgreSQL database.
- `shoppingapi-deployment.yaml`: This file creates the deployment for the backend.
- `shoppingapi-service.yaml`: This file creates the service for the backend.
- `shoppingapi-ingress.yaml`: This file creates the ingress for the backend.

### Running the project in Kubernetes

To run the project in Kubernetes, you need to have a Kubernetes cluster running.
Kind is already configured in the `.devcontainer/Dockerfile` file.
To run the project in Kubernetes, run the following command in the root directory of the repository:

```bash
kind create cluster --name ds-cluster
kubectl cluster-info --context kind-ds-cluster
kubectl get nodes
```

This will create a Kubernetes cluster with the name `ds-cluster`.
To apply the Kubernetes configuration, run the following command in the root directory of the repository:

```bash
kubectl apply -f k8s/namespace.yaml
kubectl apply -f k8s/postgresql-secret.yaml
kubectl apply -f k8s/postgresql-pvc.yaml
kubectl apply -f k8s/postgresql-deployment.yaml
kubectl apply -f k8s/postgresql-service.yaml
kubectl apply -f k8s/shoppingapi-deployment.yaml
kubectl apply -f k8s/shoppingapi-service.yaml
kubectl apply -f k8s/shoppingapi-ingress.yaml

kubectl get all -n distributed-systems
```

This will create the namespace for the backend and the PostgreSQL database.

Check the status of the pods with the following command:

```bash
kubectl rollout status deployment/shoppingapi -n distributed-systems
```

See the logs of the pods with the following command:

```bash
kubectl logs -f deployment/shoppingapi -n distributed-systems
```