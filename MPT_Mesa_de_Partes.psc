Algoritmo MesaDePartesMPT
	Definir total, CAPACIDAD Como Entero
	Definir opMenu Como Caracter
	Definir codigos, solicitantes, tipos, asuntos, fechas, estados, tiposTramite, listaEstados Como Caracter
	Dimension codigos[100]
	Dimension solicitantes[100]
	Dimension tipos[100]
	Dimension asuntos[100]
	Dimension fechas[100]
	Dimension estados[100]
	Dimension tiposTramite[4]
	Dimension listaEstados[3]
	
	total <- 0
	CAPACIDAD <- 100
	opMenu <- "0"
	
	tiposTramite[1] <- "Solicitud administrativa general (FUT)"
	tiposTramite[2] <- "Licencia de funcionamiento"
	tiposTramite[3] <- "Recurso o impugnacion"
	tiposTramite[4] <- "Acceso a la informacion publica"
	
	listaEstados[1] <- "Recibido"
	listaEstados[2] <- "En revision"
	listaEstados[3] <- "Atendido"
	
	CargarArchivo(codigos, solicitantes, tipos, asuntos, fechas, estados, total, CAPACIDAD)
	
	Mientras opMenu <> "8" Hacer
		MostrarMenu
		Escribir "Elija una opcion:"
		Leer opMenu
		
		Segun opMenu Hacer
			"1":
				Registrar(codigos, solicitantes, tipos, asuntos, fechas, estados, tiposTramite, total, CAPACIDAD)
			"2":
				BuscarExpediente(codigos, solicitantes, tipos, asuntos, fechas, estados, total)
			"3":
				Listar(codigos, solicitantes, tipos, asuntos, fechas, estados, total)
			"4":
				Ordenar(codigos, solicitantes, tipos, asuntos, fechas, estados, total)
			"5":
				ModificarExpediente(codigos, solicitantes, tipos, asuntos, fechas, estados, listaEstados, total)
			"6":
				EliminarExpediente(codigos, solicitantes, tipos, asuntos, fechas, estados, total)
			"7":
				ResumenPorTipoYEstado(tipos, estados, tiposTramite, listaEstados, total)
			"8":
				GuardarArchivo(codigos, solicitantes, tipos, asuntos, fechas, estados, total)
				Escribir "Fin del programa."
			De Otro Modo:
				Escribir "ERROR: opcion invalida. Elija un numero del 1 al 8."
		FinSegun
	FinMientras
FinAlgoritmo

//funciones
Funcion resultado <- ValidarFecha(fecha)
	Definir resultado, soloDigitos Como Logico 
	Definir i, dia, mes, anio, diasMes Como Entero
	resultado <- Verdadero
	
	Si Longitud(fecha) <> 10 Entonces
		resultado <- Falso
	SiNo
		Si Subcadena(fecha,3,3) <> "/" O Subcadena(fecha,6,6) <> "/" Entonces 
			resultado <- Falso	
		SiNo
			soloDigitos <- Verdadero
			Para i <- 1 Hasta 10 Con Paso 1 Hacer
				Si i <> 3 Y i <> 6 Entonces
					Si NO EsDigito(Subcadena(fecha,i,i)) Entonces
						soloDigitos <- Falso
					FinSi
				FinSi
			FinPara
			
			Si NO soloDigitos Entonces
				resultado <- Falso
			SiNo
				dia <- ConvertirANumero(Subcadena(fecha,1,2))
				mes <- ConvertirANumero(Subcadena(fecha,4,5))
				anio <- ConvertirANumero(Subcadena(fecha,7,10))
				
				Si anio < 1900 O anio > 2100 O mes < 1 O mes > 12 Entonces
					resultado <- Falso
				SiNo
					Segun mes Hacer
						2:
							Si EsBisiesto(anio) Entonces  
								diasMes <- 29
							SiNo
								diasMes <- 28
							FinSi
						4, 6, 9, 11:
							diasMes <- 30
						De Otro Modo:
							diasMes <- 31
					FinSegun
					Si dia < 1 O dia > diasMes Entonces
						resultado <- Falso
					FinSi
				FinSi
			FinSi
		FinSi
	FinSi
FinFuncion
Funcion resultado <- PedirFecha
	Definir resultado Como Caracter
	Definir valida Como Logico
	
	Repetir
		Escribir "Fecha de recepcion (DD/MM/AAAA):"
		Leer resultado
		valida <- ValidarFecha(resultado)
		
		Si NO valida Entonces
			Escribir "Error: fecha invalida. Use el formato DD/MM/AAAA."
		FinSi
	Hasta Que valida
FinFuncion



Funcion resultado <- EsBisiesto (year)
	Definir resultado como logico
	Si (year MOD 4 = 0 y year MOD 100 <> 0) o (year MOD 400 = 0) Entonces
		resultado <- Verdadero
	SiNo
		resultado <- Falso
	FinSi
FinFuncion 

Funcion resultado <- EsDigito(caractr)
	Definir resultado Como Logico
	Si (caractr >= "0" y caractr <= "9") y Longitud(caractr)= 1 Entonces
		resultado <- Verdadero
	SiNo
		resultado <- Falso
	FinSi
FinFuncion

//El formato de fecha debe ser DD/MM/AAAA(de entrada)
Funcion resultado <- FechaANumero(fecha)
	Definir resultado,dia, mes, anio Como Entero
	dia <-ConvertirANumero(SubCadena(fecha,1,2))
	mes <-ConvertirANumero(SubCadena(fecha,4,5))
	anio <-ConvertirANumero(SubCadena(fecha,7,10))
	mes <- mes * 100
	anio <- anio * 10000
	resultado <-anio+mes+dia
FinFuncion

Funcion resultado <- BuscarPosicion(code,total,codeABuscar)
	Definir resultado Como Entero
	resultado <- BuscarRecursivo(code, codeABuscar, 1, total)
FinFuncion

Funcion resultado <- BuscarRecursivo(code, codigo, inicio, total)
	Definir resultado Como Entero
	Si inicio > total Entonces
		resultado <- -1
	Sino
		Si code[inicio] = codigo Entonces
			resultado <- inicio
		Sino
			resultado <- BuscarRecursivo(code, codigo, inicio + 1, total)
		FinSi
	FinSi
FinFuncion

Funcion resultado <- Confirmar(pregunta)
	Definir resultado Como Logico
	Definir resp Como Caracter
	resultado <- Falso
	Repetir
		Escribir pregunta, " (S/N):"
		Leer resp
		resp <- Mayusculas(resp)
		Si resp = "S" Entonces
			resultado <- Verdadero
		Sino
			Si resp <> "N" Entonces
				Escribir "Error: responda S o N."
			FinSi
		FinSi
	Hasta Que resp = "S" O resp = "N"
FinFuncion

Funcion resultado <- PedirEstado(listaEstados)
	Definir resultado Como Caracter
	Definir i, opcn Como Entero
	Repetir
		Para i <- 1 Hasta 3
			Escribir i, ". ", listaEstados[i]
		FinPara
		Escribir "Elija el nuevo estado (1-3):"
		Leer opcn
		Si opcn < 1 O opcn > 3 Entonces
			Escribir "Error: opcion invalida. Elija un numero del 1 al 3."
		FinSi
	Hasta Que opcn >= 1 Y opcn <= 3
	resultado <- listaEstados[opcn]
FinFuncion

Funcion resultado <- PedirTipoTramite(tiposTramite)
	Definir resultado Como Caracter
	Definir i, opcn Como Entero
	
	Repetir
		Para i <- 1 Hasta 4 Con Paso 1 Hacer
			Escribir i, ". ", tiposTramite[i]
		FinPara
		
		Escribir "Elija el tipo de tramite (1-4):"
		Leer opcn
		Si opcn < 1 O opcn > 4 Entonces
			Escribir "Error: opcion invalida. Elija un numero del 1 al 4."
		FinSi
		
	Hasta Que opcn >= 1 Y opcn <= 4
	
	resultado <- tiposTramite[opcn]
FinFuncion

Funcion resultado <- ValidarFormatoCodigo (codigo)
	Definir resultado como Logico
	Definir i Como Entero
	resultado <- Verdadero
	
	Si Longitud(codigo) <> 7 Entonces
		resultado <- Falso
	SiNo
		Si Subcadena(codigo,1,4) <> "EXP-" Entonces
			resultado <- Falso
		SiNo
			Para i <- 5 Hasta 7 Con Paso 1 Hacer
				Si NO EsDigito(subcadena(codigo,i,i)) Entonces
					resultado <- Falso
				FinSi
			FinPara
		FinSi
	FinSi
FinFuncion

Funcion resultado <- PedirTextoObligatorio(mensaje)
    Definir resultado Como Caracter
	
    Repetir
        Escribir mensaje
        Leer resultado
		
        Si resultado = "" Entonces
            Escribir "Error: este campo es obligatorio."
            Escribir "Por favor, ingrese un dato."
        FinSi
		
    Hasta Que resultado <> ""
FinFuncion	

SubProceso MostrarMenu 
	Escribir "Mesa de Partes de la MPT"
	Escribir "1. Registrar expediente"
	Escribir "2. Buscar expediente por codigo"
	Escribir "3. Listar expedientes"
	Escribir "4. Ordenar expedientes"
	Escribir "5. Modificar expediente"
	Escribir "6. Eliminar expediente"
	Escribir "7. Resumen por tipo y estado"
	Escribir "8. Salir"
FinSubProceso

//subprocesos
SubProceso MostrarExpediente (codigos, solicitantes, tipos, asuntos, fechas, estados, pos)
	Escribir "codigo: ", codigos[pos]
	Escribir "solicitante: ", solicitantes[pos]
	Escribir "tipo: ", tipos[pos]
	Escribir "asunto: ", asuntos[pos]
	Escribir "fecha: ", fechas[pos]
	Escribir "Estado: ", estados[pos]
	Escribir "Posicion: ", pos
	Escribir "================="
FinSubProceso

SubProceso Listar (codigos, solicitantes, tipos, asuntos, fechas, estados, total)
	Definir i Como entero
	si total = 0 Entonces
		Escribir "La lista esta vacia"
	SiNo
		Para i <- 1 Hasta total Con Paso 1 hacer
			MostrarExpediente(codigos, solicitantes, tipos, asuntos, fechas, estados, i)
			
		FinPara
	FinSi
FinSubProceso

SubProceso BuscarExpediente(codigos, solicitantes, tipos, asuntos, fechas, estados, total)
	Definir cod Como Caracter
	Definir pos como entero
	
	cod <- PedirTextoObligatorio("¿Cual es su codigo?")
	cod <- Mayusculas(cod)
	pos <- BuscarPosicion(codigos,total,cod)
	
	Si pos = -1 Entonces
		Escribir "No se encontro el codigo"
	SiNo
		MostrarExpediente(codigos, solicitantes, tipos, asuntos, fechas, estados, pos)
	FinSi
FinSubProceso

subProceso Intercambiar (codigos, solicitantes, tipos, asuntos, fechas, estados,i,menoor)
	IntercambioDatos(codigos,i,menoor)
	IntercambioDatos(solicitantes,i,menoor)
	IntercambioDatos(tipos,i,menoor)
	IntercambioDatos(asuntos,i,menoor)
	IntercambioDatos(fechas,i,menoor)
	IntercambioDatos(estados,i,menoor)
FinSubProceso

SubProceso IntercambioDatos (arreglo, i,j)
	Definir aux Como Caracter
	aux <- arreglo[i]
	arreglo[i] <- arreglo [j]
	arreglo[j] <- aux	
FinSubProceso

SubProceso OrdenarPorCodigo(codigos, solicitantes, tipos, asuntos, fechas, estados, total)
	Definir i, j, menoor Como Entero
	
	Para i <- 1 Hasta total - 1 Con Paso 1 Hacer
		menoor <- i
		Para j <- i + 1 Hasta total Con Paso 1 Hacer
			Si codigos[j] < codigos[menoor] Entonces
				menoor <- j
			FinSi
		FinPara
		Si menoor <> i Entonces
			Intercambiar(codigos, solicitantes, tipos, asuntos, fechas, estados, i,menoor)
		FinSi
	FinPara
FinSubProceso

SubProceso OrdenarPorFecha(codigos, solicitantes, tipos, asuntos, fechas, estados, total)
	Definir i, j, menoor Como Entero
	
	Para i <- 1 Hasta total - 1 Con Paso 1 Hacer
		menoor <- i
		Para j <- i + 1 Hasta total Con Paso 1 Hacer
			Si FechaANumero(fechas[j]) < FechaANumero(fechas[menoor]) Entonces
				menoor <- j
			FinSi
		FinPara
		Si menoor <> i Entonces
			Intercambiar(codigos, solicitantes, tipos, asuntos, fechas, estados, i, menoor)
		FinSi
	FinPara
FinSubProceso

SubProceso Ordenar(codigos, solicitantes, tipos, asuntos, fechas, estados, total)
	Definir criterio Como Entero
	
	Si total = 0 Entonces
		Escribir "La lista esta vacia. No hay nada que ordenar."
	SiNo
		Repetir
			Escribir "1. Ordenar por codigo"
			Escribir "2. Ordenar por fecha de recepcion"
			Escribir "Elija el criterio (1-2):"
			Leer criterio
			Si criterio <> 1 Y criterio <> 2 Entonces
				Escribir "Ingrese un criterio valido"
			FinSi
		Hasta Que criterio = 1 O criterio = 2
		
		Si criterio = 1 Entonces
			OrdenarPorCodigo(codigos, solicitantes, tipos, asuntos, fechas, estados, total)
		SiNo
			OrdenarPorFecha(codigos, solicitantes, tipos, asuntos, fechas, estados, total)
		FinSi
		
		Listar(codigos, solicitantes, tipos, asuntos, fechas, estados, total)
	FinSi
FinSubProceso

SubProceso Registrar(codigos, solicitantes, tipos, asuntos, fechas, estados, tiposTramite, total Por Referencia, CAPACIDAD)
	Definir codigo, solicitante, tipo, asunto, fecha Como Caracter
	
	Si total >= CAPACIDAD Entonces
		Escribir "ERROR: capacidad completa."
	SiNo
		codigo <- PedirTextoObligatorio("Codigo (ej. EXP-001):")
		codigo <- Mayusculas(codigo)
		
		Si NO ValidarFormatoCodigo(codigo) Entonces
			Escribir "ERROR: el codigo debe tener el formato EXP-001. No se registro."
		SiNo
			Si BuscarPosicion(codigos, total, codigo) <> -1 Entonces
				Escribir "ERROR: el codigo ya existe. No se registro."
			SiNo
				solicitante <- PedirTextoObligatorio("Solicitante (ficticio):")
				tipo <- PedirTipoTramite(tiposTramite)
				asunto <- PedirTextoObligatorio("Asunto:")
				fecha <- PedirFecha
				
				total <- total + 1
				codigos[total] <- codigo
				solicitantes[total] <- solicitante
				tipos[total] <- tipo
				asuntos[total] <- asunto
				fechas[total] <- fecha
				estados[total] <- "Recibido"
				
				Escribir "Expediente ", codigo, " registrado correctamente."
			FinSi
		FinSi
	FinSi
FinSubProceso

SubProceso GuardarArchivo(codigos, solicitantes, tipos, asuntos, fechas, estados, total)
	//PSeInt no tiene archivos: se escribe una linea por expediente en expedientes.txt
	Escribir "Expedientes guardados: ", total
FinSubProceso

SubProceso CargarArchivo(codigos, solicitantes, tipos, asuntos, fechas, estados, total Por Referencia, CAPACIDAD)
	//PSeInt no tiene archivos: se lee expedientes.txt y se separa cada linea por "|"
	total <- 0
	Escribir "No hay archivo previo. Se inicia con la lista vacia."
FinSubProceso

SubProceso ModificarExpediente(codigos, solicitantes, tipos, asuntos, fechas, estados, listaEstados, total)
	Definir cod, op, nuevo Como Caracter
	Definir pos Como Entero
	Si total = 0 Entonces
		Escribir "No hay expedientes para modificar."
	Sino
		cod <- Mayusculas(PedirTextoObligatorio("Codigo del expediente a modificar:"))
		pos <- BuscarPosicion(codigos, total, cod)
		Si pos = -1 Entonces
			Escribir "No se encontro el codigo"
		Sino
			MostrarExpediente(codigos, solicitantes, tipos, asuntos, fechas, estados, pos)
			Escribir "1. Modificar estado"
			Escribir "2. Modificar asunto"
			Escribir "Elija una opcion (1-2):"
			Leer op
			Si op = "1" Entonces
				nuevo <- PedirEstado(listaEstados)
			Sino
				Si op = "2" Entonces
					nuevo <- PedirTextoObligatorio("Nuevo asunto:")
				Sino
					Escribir "Error: Opcion invalida."
				FinSi
			FinSi
			Si op = "1" O op = "2" Entonces
				Si Confirmar("Confirma el cambio?") Entonces
					Si op = "1" Entonces
						estados[pos] <- nuevo
					Sino
						asuntos[pos] <- nuevo
					FinSi
					GuardarArchivo(codigos, solicitantes, tipos, asuntos, fechas, estados, total)
					Escribir "Expediente ", cod, " modificado correctamente."
				Sino
					Escribir "Modificacion cancelada."
				FinSi
			FinSi
		FinSi
	FinSi
FinSubProceso

SubProceso EliminarExpediente(codigos, solicitantes, tipos, asuntos, fechas, estados, total Por Referencia)
	Definir cod Como Caracter
	Definir pos, i Como Entero
	Si total = 0 Entonces
		Escribir "No hay expedientes para eliminar."
	Sino
		cod <- Mayusculas(PedirTextoObligatorio("Codigo del expediente a eliminar:"))
		pos <- BuscarPosicion(codigos, total, cod)
		Si pos = -1 Entonces
			Escribir "No se encontro el codigo"
		Sino
			MostrarExpediente(codigos, solicitantes, tipos, asuntos, fechas, estados, pos)
			Si Confirmar("Confirma eliminar el expediente " + cod + "?") Entonces
				Para i <- pos Hasta total - 1
					codigos[i] <- codigos[i + 1]
					solicitantes[i] <- solicitantes[i + 1]
					tipos[i] <- tipos[i + 1]
					asuntos[i] <- asuntos[i + 1]
					fechas[i] <- fechas[i + 1]
					estados[i] <- estados[i + 1]
				FinPara
				codigos[total] <- ""
				solicitantes[total] <- ""
				tipos[total] <- ""
				asuntos[total] <- ""
				fechas[total] <- ""
				estados[total] <- ""
				total <- total - 1
				GuardarArchivo(codigos, solicitantes, tipos, asuntos, fechas, estados, total)
				Escribir "Expediente ", cod, " eliminado correctamente."
			Sino
				Escribir "Eliminacion cancelada."
			FinSi
		FinSi
	FinSi
FinSubProceso

SubProceso ResumenPorTipoYEstado(tipos, estados, tiposTramite, listaEstados, total)
	Definir conteo Como Entero
	Definir i, j, fila, col, sinClasificar, totalFila, totalGeneral Como Entero
	Dimension conteo[4,3]
	Si total = 0 Entonces
		Escribir "No hay expedientes para resumir."
	Sino
		Para i <- 1 Hasta 4
			Para j <- 1 Hasta 3
				conteo[i,j] <- 0
			FinPara
		FinPara
		sinClasificar <- 0
		Para i <- 1 Hasta total
			fila <- BuscarPosicion(tiposTramite, 4, tipos[i])
			col <- BuscarPosicion(listaEstados, 3, estados[i])
			Si fila <> -1 Y col <> -1 Entonces
				conteo[fila,col] <- conteo[fila,col] + 1
			Sino
				sinClasificar <- sinClasificar + 1
			FinSi
		FinPara
		Escribir "Resumen de expedientes por tipo de tramite y estado"
		totalGeneral <- 0
		Para i <- 1 Hasta 4
			totalFila <- 0
			Escribir tiposTramite[i]
			Para j <- 1 Hasta 3
				Escribir "   ", listaEstados[j], ": ", conteo[i,j]
				totalFila <- totalFila + conteo[i,j]
			FinPara
			Escribir "   Total del tipo: ", totalFila
			totalGeneral <- totalGeneral + totalFila
		FinPara
		Escribir "Total general: ", totalGeneral
		Si sinClasificar > 0 Entonces
			Escribir "Sin clasificar: ", sinClasificar
		FinSi
	FinSi
FinSubProceso
