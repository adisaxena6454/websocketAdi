var timeValue = null;
function getmessage() {
    var socket = io("http://13.232.18.39/");
    
    document.getElementById("btStart").setAttribute("hidden", "");
    document.getElementById("btStop").removeAttribute("hidden");
    socket.on('dashboard', function (data) {
        timeValue = setInterval(function () {
            addMessage(data.Result);
        }, 1000);

    });

    socket.on('error', console.error.bind(console));
    socket.on('message', console.log.bind(console));

    function addMessage(message) {
        for (var i = 0; i <= message.length; i++) {
            var messageRsp = JSON.stringify(message[i]);
            if (messageRsp) {
                var html = "<tr><td>" + message[i].InstrumentIdentifier + "</td><td>" + message[i].LastTradePrice + "</td><td>" + message[i].PriceChange + "</td><td>" + message[i].PriceChangePercentage + "</td></tr>";
                $("#tblBykeLists").append(html);

            }
        }

    }
}
function haltFunction() {
    location.reload();

}

    

