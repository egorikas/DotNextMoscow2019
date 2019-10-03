package main

import (
	"net/http"

	"github.com/opentracing/opentracing-go"
	"github.com/opentracing/opentracing-go/ext"
	"github.com/uber/jaeger-lib/metrics"

	"github.com/uber/jaeger-client-go"
	jaegercfg "github.com/uber/jaeger-client-go/config"
	jaegerlog "github.com/uber/jaeger-client-go/log"
)

func main() {
	InitJaeger("alien")
	println("alien на 8000 порту")
	http.HandleFunc("/api/number", AlienServer)
	err := http.ListenAndServe(":8000", nil)
	if err != nil{
		panic(err)
	}
}

func AlienServer(w http.ResponseWriter, r *http.Request) {
	spanCtx, _ := opentracing.GlobalTracer().Extract(opentracing.HTTPHeaders, opentracing.HTTPHeadersCarrier(r.Header))
	serverSpan := opentracing.GlobalTracer().StartSpan("Alien server говорит привет", ext.RPCServerOption(spanCtx))
	defer serverSpan.Finish()

	h := w.Header()
	h.Set("Content-Type", "text/html")
	w.WriteHeader(200)
	_, _ = w.Write([]byte("👽"))
}



func InitJaeger(serviceName string) {
	cfg := jaegercfg.Configuration{
		ServiceName: serviceName,
		Sampler:     &jaegercfg.SamplerConfig{
			Type:  jaeger.SamplerTypeConst,
			Param: 1,
		},
		Reporter:    &jaegercfg.ReporterConfig{
			LogSpans: true,
		},
	}

	jLogger := jaegerlog.StdLogger
	jMetricsFactory := metrics.NullFactory

	// Initialize tracer with a logger and a metrics factory
	tracer, _, _ := cfg.NewTracer(
		jaegercfg.Logger(jLogger),
		jaegercfg.Metrics(jMetricsFactory),
	)
	opentracing.SetGlobalTracer(tracer)
}
