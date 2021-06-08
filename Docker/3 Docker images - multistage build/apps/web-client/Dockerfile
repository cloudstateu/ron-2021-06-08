FROM microsoft/dotnet:2.2-sdk
COPY . ./web_client
WORKDIR /web_client/
RUN dotnet build -c Release
ENTRYPOINT ["dotnet", "run", "-c", "Release", "--no-build"]