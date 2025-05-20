# Distributed Systems

This repository contains the code for the Distributed Systems course at Hochschule Esslingen.
It implements the required backend for the course project.

The API which is provided by the backend is defined in the `REST_API.yml` file which is located in the root directory of the repository.
The backend is implemented in DotNet Core 9.0 and will communicate with a PostgreSQL database.

---

## Setup the devcontainer

In the `.devcontainer` directory you can find the configuration for the devcontainer. This allows you to run everything in GitHub Codespaces or locally in a Docker container with all required tooling pre-installed.

## Project Structure

- **.devcontainer/**: Configuration for the development container.
- **backend/ShoppingApi/**: Backend code (ASP.NET Core 9.0)

  - **Controllers/**: API controllers (e.g., ItemController).
  - **Data/**: EF Core database context.
  - **DTOs/**: Data Transfer Objects for API payloads.
  - **Models/**: Domain models.
  - **Properties/**: Launch settings.
  - **Dockerfile**: Builds the backend Docker image.
  - **Program.cs**: Entry point.
  - **ShoppingApi.csproj**: Project file with dependencies.

- **.gitignore**: Git ignore rules.
- **Distributed-Systems.sln**: Visual Studio solution file.
- **docker-compose.yml**: Defines services for the backend, database, and course-provided frontend.
- **README.md**: This documentation.
- **REST_API.yml**: OpenAPI/Swagger spec for the backend API.
- **kubernetes/**: Kubernetes manifests for Pods/Deployments and Services.
- **kind-config.yaml**: Kind cluster configuration.

---

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

---

## Running Kubernetes

The Kubernetes configuration is located in the `kubernetes` directory.
The `kubernetes` directory contains the configuration for the Kubernetes deployment and service.

To run the project in Kubernetes, you need to have a Kubernetes cluster running.
Kind is already configured in the `.devcontainer/Dockerfile` file.

## Running in Kubernetes (Kind)

The `kubernetes/` directory contains your manifests for running the application on a Kubernetes cluster. We use [Kind](https://kind.sigs.k8s.io/) for local testing.

### Create a Kind cluster

Use the provided `kind-config.yaml` to spin up a local cluster:

```bash
kind create cluster --config kind-config.yaml
kubectl cluster-info
```

### Deploy the application manifests

Apply all Kubernetes manifests (Pods/Deployments and Services):

```bash
kubectl apply -f kubernetes/
```

If you have raw Pod manifests, consider replacing them with the provided `deploy-*.yaml` Deployment manifests for self-healing and scaling.

### Verify the deployment

Check that all Pods and Services are running:

```bash
kubectl get pods,svc -o wide
```

Look for the following:

- Pods: `backend`, `database`, `frontend` (or Deployments thereof)
- Services: `backend`, `database`, `frontend` (ClusterIP)

### Port-forward to access services locally

- **Backend**:

  ```bash
  kubectl port-forward svc/backend 3000:3000
  ```

- **Frontend**:

  ```bash
  kubectl port-forward svc/frontend 5000:5000
  ```

### Debugging & logs

- View Pod logs:

  ```bash
  kubectl logs <pod-name>
  ```

- Describe resources/events:

  ```bash
  kubectl describe pod/frontend
  kubectl describe service/database
  ```

- Exec into a running container:

  ```bash
  kubectl exec -it <pod-name> -- sh
  ```

---

## Common Kubernetes Commands

| Purpose                | Command                                                   |
| ---------------------- | --------------------------------------------------------- |
| Create Kind cluster    | `kind create cluster --config kind-config.yaml`           |
| Show cluster info      | `kubectl cluster-info`                                    |
| Apply manifests        | `kubectl apply -f <dir-or-file>`                          |
| Get pods & services    | `kubectl get pods,svc`                                    |
| Describe a resource    | `kubectl describe <type>/<name>`                          |
| View logs              | `kubectl logs <pod>`                                      |
| Exec into pod          | `kubectl exec -it <pod> -- /bin/sh`                       |
| Port-forward service   | `kubectl port-forward svc/<name> <local>:<remote>`        |
| Scale a Deployment     | `kubectl scale deployment/<name> --replicas=<n>`          |
| Update image (rolling) | `kubectl set image deployment/<name> <container>=<image>` |
| Restart a Deployment   | `kubectl rollout restart deployment/<name>`               |
| Roll back a Deployment | `kubectl rollout undo deployment/<name>`                  |

