# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

RUN apt-get update -y \
  && apt-get -y install \
    xvfb \
  && rm -rf /var/lib/apt/lists/* /var/cache/apt/*

 # Install Chrome
 RUN apt-get update && apt-get install -y \
 apt-transport-https \
 ca-certificates \
 curl \
 gnupg \
 hicolor-icon-theme \
 libcanberra-gtk* \
 libgl1-mesa-dri \
 libgl1-mesa-glx \
 libpango1.0-0 \
 libpulse0 \
 libv4l-0 \
 fonts-symbola \
 libnss3-tools \
 --no-install-recommends \
 && curl -sSL https://dl.google.com/linux/linux_signing_key.pub | apt-key add - \
 && echo "deb [arch=amd64] https://dl.google.com/linux/chrome/deb/ stable main" > /etc/apt/sources.list.d/google.list \
 && apt-get update && apt-get install -y \
 google-chrome-stable \
 --no-install-recommends \
 && apt-get purge --auto-remove -y curl \
 && rm -rf /var/lib/apt/lists/*
 # End Chrome

# Set display port and dbus env to avoid hanging
ENV DISPLAY=:99
ENV DBUS_SESSION_BUS_ADDRESS=/dev/null

COPY sshd_config /etc/ssh/
COPY init.sh /usr/local/bin/

WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 9000

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TESTWEB/TESTWEB.csproj", "TESTWEB/"]
RUN dotnet restore "./TESTWEB/TESTWEB.csproj"
COPY . .
WORKDIR "/src/TESTWEB"
RUN dotnet build "./TESTWEB.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TESTWEB.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TESTWEB.dll"]