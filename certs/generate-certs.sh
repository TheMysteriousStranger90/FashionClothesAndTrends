#!/usr/bin/env bash
# Generates HTTPS certificates for local development.
# Creates:
#   src/ClientApp/ssl/cert.pem and key.pem  (Angular dev server)
#   localhost.pfx                             (ASP.NET Core backend)
# Usage: ./generate-certs.sh <pfx-password>

set -e

if [ -z "$1" ]; then
  echo "Usage: $0 <pfx-password>"
  exit 1
fi

PASSWORD="$1"
ROOT="$(cd "$(dirname "$0")/.." && pwd)"
SSL_DIR="$ROOT/src/ClientApp/ssl"
CERT_PEM="$SSL_DIR/cert.pem"
KEY_PEM="$SSL_DIR/key.pem"
PFX_PATH="$ROOT/localhost.pfx"

mkdir -p "$SSL_DIR"

echo "Generating self-signed certificate for localhost..."
openssl req -x509 -newkey rsa:4096 -sha256 -days 365 -nodes \
    -keyout "$KEY_PEM" \
    -out "$CERT_PEM" \
    -subj "/CN=localhost" \
    -addext "subjectAltName=DNS:localhost,IP:127.0.0.1"

echo "Exporting as PFX for ASP.NET Core backend..."
openssl pkcs12 -export \
    -out "$PFX_PATH" \
    -inkey "$KEY_PEM" \
    -in "$CERT_PEM" \
    -passout "pass:$PASSWORD"

echo ""
echo "Done!"
echo "  cert.pem -> $CERT_PEM"
echo "  key.pem  -> $KEY_PEM"
echo "  PFX      -> $PFX_PATH"
echo ""
echo "Add to your .env file:"
echo "  ASPNETCORE_Kestrel__Certificates__Default__Password=$PASSWORD"
echo "  ASPNETCORE_Kestrel__Certificates__Default__Path=/https/localhost.pfx"