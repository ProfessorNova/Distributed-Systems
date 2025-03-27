# .devcontainer/Dockerfile
FROM mcr.microsoft.com/vscode/devcontainers/dotnet:9.0

# Als Root: Workloads aktualisieren
USER root
RUN dotnet workload update

# (Optional) Node.js installieren, falls noch nicht vorhanden
RUN apt-get update && apt-get install -y nodejs npm

# Falls erforderlich, Angular CLI installieren
RUN npm install -g @angular/cli

WORKDIR /workspace
