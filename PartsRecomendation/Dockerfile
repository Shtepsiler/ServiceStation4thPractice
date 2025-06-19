# Multi-stage Dockerfile for Auto Parts Classification API
FROM python:3.9-slim as base

# Install system dependencies (cached layer)
RUN apt-get update && apt-get install -y \
    gcc \
    g++ \
    curl \
    && rm -rf /var/lib/apt/lists/*

# =============================================
# Stage 1: Dependencies (rarely changes)
# =============================================
FROM base as dependencies

WORKDIR /app

# Copy requirements first for better Docker layer caching
COPY requirements.txt .

# Install Python dependencies (this layer will be cached unless requirements.txt changes)
RUN pip install --no-cache-dir --upgrade pip && \
    pip install --no-cache-dir -r requirements.txt

# =============================================
# Stage 2: Development (for fast rebuilds)
# =============================================
FROM dependencies as development

WORKDIR /app

# Set environment variables
ENV PYTHONPATH=/app
ENV PYTHONUNBUFFERED=1
ENV MODEL_PATH=/app/model_lstm_best.pth
ENV REDIS_HOST=rediscash
ENV REDIS_PORT=6379
ENV REDIS_DB=0

# Create non-root user FIRST
RUN useradd --create-home --shell /bin/bash --uid 1000 appuser

# Create necessary directories with proper ownership
RUN mkdir -p /app/models /app/logs /app/data /app/backups /tmp/model_backups && \
    chown -R appuser:appuser /app && \
    chmod -R 755 /app

# Copy source code (this layer changes frequently)
COPY --chown=appuser:appuser . .

# Ensure model file is writable
RUN if [ -f /app/model_lstm_best.pth ]; then \
        chmod 664 /app/model_lstm_best.pth; \
    fi

USER appuser

# Expose port
EXPOSE 8000

# Development command with auto-reload
CMD ["uvicorn", "main:app", "--host", "0.0.0.0", "--port", "8000", "--reload", "--workers", "1"]

# =============================================
# Stage 3: Production (optimized)
# =============================================
FROM dependencies as production

WORKDIR /app

# Set production environment variables
ENV PYTHONPATH=/app
ENV PYTHONUNBUFFERED=1
ENV MODEL_PATH=/app/model_lstm_best.pth
ENV REDIS_HOST=rediscash
ENV REDIS_PORT=6379
ENV REDIS_DB=0

# Create non-root user FIRST
RUN useradd --create-home --shell /bin/bash --uid 1000 appuser

# Create necessary directories with proper ownership
RUN mkdir -p /app/models /app/logs /app/data /app/backups /tmp/model_backups && \
    chown -R appuser:appuser /app && \
    chmod -R 755 /app

# Copy source code
COPY --chown=appuser:appuser . .

# Ensure model file is writable
RUN if [ -f /app/model_lstm_best.pth ]; then \
        chmod 664 /app/model_lstm_best.pth; \
    fi

USER appuser

# Expose port
EXPOSE 8000

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=60s --retries=3 \
    CMD curl -f http://localhost:8000/ || exit 1

# Production command
CMD ["uvicorn", "main:app", "--host", "0.0.0.0", "--port", "8000", "--workers", "1"]