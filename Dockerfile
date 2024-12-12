FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build_core_nuget
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Chargily.Pay/Chargily.Pay.csproj", "Chargily.Pay/"]
RUN dotnet restore "Chargily.Pay/Chargily.Pay.csproj"
COPY . .
WORKDIR "/src/Chargily.Pay"
RUN dotnet build -c $BUILD_CONFIGURATION --no-restore 
RUN dotnet pack -c $BUILD_CONFIGURATION -o /app/publish --no-build

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build_aspnet_extension_nuget
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Chargily.Pay/Chargily.Pay.csproj", "Chargily.Pay/"]
COPY ["Chargily.Pay.AspNet/Chargily.Pay.AspNet.csproj", "Chargily.Pay.AspNet/"]
RUN dotnet restore "Chargily.Pay.AspNet/Chargily.Pay.AspNet.csproj"
COPY . .
WORKDIR "/src/Chargily.Pay.AspNet"
RUN dotnet build -c $BUILD_CONFIGURATION --no-restore 
RUN dotnet pack -c $BUILD_CONFIGURATION -o /app/publish --no-build

FROM scratch
COPY --from=build_core_nuget /app/publish/ /
COPY --from=build_aspnet_extension_nuget /app/publish/ /
