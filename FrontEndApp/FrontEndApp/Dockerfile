FROM microsoft/aspnetcore:2.0
ARG source
WORKDIR /app
EXPOSE 80
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "FrontEndApp.dll"]
ENV AppSettings deployment
ENV ASPNETCORE_ENVIRONMENT Production