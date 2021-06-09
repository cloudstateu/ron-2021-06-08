from flask import Flask
app = Flask('webapp')

@app.route('/')
def appVersion():
  return "This is Python Web App - Version 1\n"

if __name__ == '__main__':
  app.run(host = '0.0.0.0', port = 8080)