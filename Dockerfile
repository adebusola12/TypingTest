FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["TypingTest.csproj", "."]
RUN dotnet restore "TypingTest.csproj"


COPY . .

RUN dotnet build "TypingTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TypingTest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TypingTest.dll"]