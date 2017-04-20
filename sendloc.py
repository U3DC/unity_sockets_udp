import socket
import time
import json
UDP_IP = "localhost"
UDP_PORT = 50000
message = "123"

print "UDP target IP:", UDP_IP
print "UDP target port:", UDP_PORT

sock = socket.socket(socket.AF_INET, # Internet
                        socket.SOCK_DGRAM) # UDP

for i in range(1501):
    message ={"x":0.1*i,"z":pow((150*150-0.01*i*i),0.5)}
    print i
    sock.sendto(json.dumps(message), (UDP_IP, UDP_PORT))
    #sock.sendto(str(i),(UDP_IP,UDP_PORT))
    time.sleep(0.01)
