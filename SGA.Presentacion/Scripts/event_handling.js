function create(start, end, resource) {
    createModal().showUrl('New.aspx?start=' + start + "&end=" + end + "&resource=" + resource);
}


function editBlock(e) {
    var start = e.start.substring(0, e.start.indexOf(':'));
    createModal().showUrl("EditBlock.aspx?id=" + start);
}

function createModal() {
    var modal = new DayPilot.Modal();
    modal.closed = function () {
        if (this.result && this.result.refresh) {
            dp.commandCallBack("refresh", { message: this.result.message });
        }
        dp.clearSelection();
    };

    return modal;
}

function ask(e) {

    // it's a normal event
    if (!e.recurrent()) {
        edit(e);
        return;
    }

    // it's a recurrent event but it's an exception from the series
    if (e.id() !== null) {
        edit(e);
        return;
    }

    var modal = new DayPilot.Modal();
    modal.closed = function () {
        if (this.result != "cancel") {
            edit(e, this.result);
        }
    };

    modal.showUrl("RecurrentEditMode.html");
}

function edit(e, mode) {
    var url = "Edit.aspx?q=1"
    if (e.recurrentMasterId()) {
        url += "&master=" + e.recurrentMasterId();
    }
    if (e.value() !== null) {
        url += "&id=" + e.id();
    }
    if (mode == "this") {
        url += "&start=" + e.start();
    }
    createModal().showUrl(url);
}