apiVersion: apps/v1
kind: Deployment
metadata:
  name: kube-cicd
  labels:
    app: kube-cicd
spec:
  replicas: 1
  selector:
    matchLabels:
      app: kube-cicd
  template:
    metadata:
      labels:
        app: kube-cicd
    spec:
      containers:
      - name: kube-cicd
        image: gcr.io/GOOGLE_CLOUD_PROJECT/kube-cicd:COMMIT_SHA
        ports:
        - containerPort: 8080
---
kind: Service
apiVersion: v1
metadata:
  name: kube-cicd
spec:
  selector:
    app: kube-cicd
  ports:
  - protocol: TCP
    port: 80
    targetPort: 8080
  type: LoadBalancer