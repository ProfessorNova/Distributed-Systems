FROM mcr.microsoft.com/devcontainers/dotnet:9.0

USER root

RUN curl -LO "https://dl.k8s.io/release/$(curl -L -s https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl"
RUN curl -LO "https://dl.k8s.io/release/$(curl -L -s https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl.sha256"
RUN echo "$(cat kubectl.sha256)  kubectl" | sha256sum --check

RUN sudo install -o root -g root -m 0755 kubectl /usr/local/bin/kubectl
RUN kubectl version --client
RUN kubectl version --client --output=yaml

RUN curl -Lo ./kind https://kind.sigs.k8s.io/dl/v0.27.0/kind-linux-amd64
RUN chmod +x ./kind
RUN sudo mv ./kind /usr/local/bin/kind
RUN kind version
