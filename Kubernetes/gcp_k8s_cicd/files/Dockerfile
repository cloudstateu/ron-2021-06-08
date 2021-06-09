FROM python:3.7-slim
RUN pip install flask
WORKDIR /app
COPY web_app.py /app/web_app.py
ENTRYPOINT ["python"]
CMD ["/app/web_app.py"]