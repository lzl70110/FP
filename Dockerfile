 
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY FP.sln ./
COPY FP.Web/FP.Web.csproj FP.Web/
COPY FP.Application/FP.Application.csproj FP.Application/
COPY FP.Domain/FP.Domain.csproj FP.Domain/
COPY FP.Infrastructure/FP.Infrastructure.csproj FP.Infrastructure/

# Restore dependencies
RUN dotnet restore FP.Web/FP.Web.csproj

# Copy source code
COPY FP.Web/ FP.Web/
COPY FP.Application/ FP.Application/
COPY FP.Domain/ FP.Domain/
COPY FP.Infrastructure/ FP.Infrastructure/

# Publish application
RUN dotnet publish FP.Web/FP.Web.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

# Render provides the PORT environment variable.
ENV ASPNETCORE_HTTP_PORTS=10000

EXPOSE 10000

ENTRYPOINT ["dotnet", "FP.Web.dll"]
 
