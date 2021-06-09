from flask import Flask, abort
import socket
import json

app = Flask(__name__)

i=0

@app.route('/')
def app1():
    global i
    i=i+1
    if i%2 != 0:
      abort(500) 
    else: 
        host_name = socket.gethostname() 
        host_ip = socket.gethostbyname(host_name) 
        data = {}
        data['app'] = "ver. 3"
        data['host_ip'] = host_ip
        data['host_name'] = host_name
        return json.dumps(data)


if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0')