var calendarEl = document.getElementById('calendar');
var calendar = new FullCalendar.Calendar(calendarEl, {
    initialView: 'dayGridMonth',
    buttonText: {
        today: 'Hoy',
        month: 'Mes',
        week: 'Semana',
        day: 'Dia',
        list: 'Lista'
    },

    headerToolbar: {
        left: 'prev,next,today',
        center: 'title',
        right: 'dayGridMonth,timeGridWeek,timeGridDay',
    },
    locale: 'es',
    editable: true,
    timeZone: 'local',
    eventDrop: function (info) {
        var maxEventDate = 0;
        const eventos = calendar.getEvents()
        for (const evento of eventos) {
            (evento.start.getTime() === info.event.start.getTime()) && maxEventDate++
            if (maxEventDate > 2) {
                break;
            }
        }

        if (maxEventDate > 2) {
            Swal.fire({
                icon: 'error',
                title: 'Error de asignacion',
                text: 'No puedes agendar mas de dos citas en la misma hora',

            })
            info.revert();
            return
        }
        const eventDate = info.event.start
        const dateFormat = FullCalendar.formatDate(eventDate,
            {
                month: '2-digit',
                year: 'numeric',
                day: '2-digit'
            })
        const hourFormat = FullCalendar.formatDate(eventDate,
            {
                hour: '2-digit',
                minute: '2-digit',
                second: '2-digit',
                hour12: false
            })

        const hoy = new Date();

        if (eventDate < hoy) {
            Swal.fire({
                icon: 'error',
                title: 'Error de asignacion',
                text: 'No puedes agendar una cita un dia antetior al de hoy',

            })
            info.revert();
            return
        }

        Swal.fire({
            title: 'Desea confirmar el cambio de fecha/horario?',

            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si, deseo cambiar la fecha/hora de la cita!'
        }).then((result) => {
            if (result.isConfirmed) {
                const objDate = {
                    id_cita: info.event.id,
                    fecha: dateFormat,
                    hora: hourFormat
                }
                updateDataAjax(objDate)


            } else {
                info.revert();
            }
        })
    }




});

function updateDataAjax(objDate) {
    obj = { fecha: objDate.fecha, hora: objDate.hora, id_cita: objDate.id_cita }
    $.ajax({
        type: "POST",
        url: "calendario.aspx/actualizarCita",
        data: JSON.stringify(obj),
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + "\n" + xhr.responseText, "\n" + thrownError)
        },

        success: function (response) {
            if (response) {
                Swal.fire(
                    'Cita reasignada!',
                    'Fecha/hora modificada con exito',
                    'success'
                )

            }
        }

    })
}

function initCalendar(eventos) {

    const hoy = new Date()
    for (let i = 0; i < eventos.length; i++) {
        const evenDateString = `${eventos[i].fechaCita} ${eventos[i].hora}`
        const evenDateObj = new Date(evenDateString)
        calendar.addEvent({
            id: eventos[i].id_cita,
            title: eventos[i].tipo_cita,
            start: evenDateString,
            backgroundColor: `${(eventos[i].tipo_cita === 'CONSULTA') ? 'blue' : (eventos[i].tipo_cita === 'OPERACION') ? 'red' : 'green'}`,
            editable: (evenDateObj < hoy) ? false : true
        })
    }
    calendar.render();
}


function sendDataAjax() {
    $.ajax({
        type: "POST",
        url: "calendario.aspx/listarCalendario",
        data: {},
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + "\n" + xhr.responseText, "\n" + thrownError)
        },

        success: function (arrObj) {
            initCalendar(arrObj.d)
        }

    })

}
sendDataAjax();
