var calendarEl = document.getElementById('calendar');
var calendar = new FullCalendar.Calendar(calendarEl, {
    initialView: 'dayGridMonth',
    headerToolbar: {
        left: 'prev,next,today',
        center: 'title',
        right: 'dayGridMonth,timeGridWeek,timeGridDay'
    }




});

function initCalendar(eventos) {

    for (let i = 0; i < eventos.length; i++) {
        calendar.addEvent({
            title: eventos[i].tipo_cita,
            start: `${eventos[i].fechaCita}T${eventos[i].hora}`,
            backgroundColor: `${(eventos[i].tipo_cita === 'CONSULTA') ? 'blue' : (eventos[i].tipo_cita === 'OPERACION') ? 'red' : 'green'}`
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