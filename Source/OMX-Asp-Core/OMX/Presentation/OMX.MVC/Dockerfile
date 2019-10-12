FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Presentation/OMX.MVC/OMX.MVC.csproj", "Presentation/OMX.MVC/"]
RUN dotnet restore "Presentation/OMX.MVC/OMX.MVC.csproj"
COPY . .
WORKDIR "/src/Presentation/OMX.MVC"
RUN dotnet build "OMX.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OMX.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OMX.MVC.dll"]