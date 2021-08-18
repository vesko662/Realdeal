setup = () => {
    var connection =
        new signalR.HubConnectionBuilder()
            .withUrl("/chat")
            .build();

    connection.on("NewMessage",
        function (message) {
            console.log(message);
            var chatInfo;
            chatInfo = `<div class="row">
                           <div class="mb-2 offset-md-2 col-md-8 border border-1 border-dark">
                            <div class="row">
                               <div style="background-color:silver"  class="col-md-2 text-center text-break border-end border-2 border-dark"><p class="float-end">${message.senderName}</p></div>
                               <div class="col-md-8 text-break"><span class="float-sm-end float-md-none float-end">${message.content}</span></div>
                               <div class="col-md-2 text-center"><span class="float-end">${message.createdOn}</span></div>
                            </div>
                          </div>
                        </div>`;
            var messagesList = document.getElementById("messages");
            messagesList.innerHTML += chatInfo;
            document.getElementById("messageInput").value = "";
        });

    $("#sendButton").click(function (data) {
        var content = $("#messageInput").val();
        content = escapeHtml(content);
        var advertId = $("#advertId").val();
        var senderId = $("#senderId").val();
        var recieverId = $("#recieverId").val();
        connection.invoke("SendMessage", advertId, senderId, recieverId, content);
    });

    connection.start().catch(function (err) {
        return console.error(err.toString());
    });

    function escapeHtml(unsafe) {
        return unsafe
            .replace(/&/g, "&amp;")
            .replace(/</g, "&lt;")
            .replace(/>/g, "&gt;")
            .replace(/"/g, "&quot;")
            .replace(/'/g, "&#039;");
    }
};
setup();
