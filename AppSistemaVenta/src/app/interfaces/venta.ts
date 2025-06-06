import { DetalleVenta } from "./detalle-venta"

export interface Venta {
    numeroDocumento?: string
    idVenta?:number,
    folioFiscal?:string,
    tipoPago:string,
    fechaRegistro?:string,
    totalTexto:string,
    detalleVenta: DetalleVenta[]
}