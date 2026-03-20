$(document).ready(function () {
    const table = $('#inspectionTable').DataTable({
        processing: true,
        serverSide: true,
        pageLength: 100,        
        ajax: {
            url: '/Admin/GetInspectionsForDataTable',
            type: 'POST'
        },
        order: [], // server-side will handle sorting
        columns: [
            {
                data: 'InspectionDocumentNo',
                render: function (data) {
                    return `<span class="text-secondary text-xs font-weight-bold">${data}</span>`;
                }
            },
            {
                data: 'InspectionDateFormatted',
                render: function (data) {
                    const d = new Date(data);
                    return `<span class="text-secondary text-xs font-weight-bold">${d.toLocaleDateString('en-US', {
                        year: 'numeric',
                        month: 'long',
                        day: 'numeric'
                    })}</span>`;
                }
            },
            {
                data: 'Customer',
                render: function (data) {
                    return `<span class="text-secondary text-xs font-weight-bold">${data ?? ''}</span>`;
                }
            },
            {
                data: 'Employee',
                render: function (data) {
                    return `<span class="text-secondary text-xs font-weight-bold">${data ?? ''}</span>`;
                }
            },
            {
                data: null,
                //render: function (data) {
                //    const combined = `${data.CustomerArea || ''}${data.CustomerArea && data.CustomerLocation ? ', ' : ''}${data.CustomerLocation || ''}`;
                //    return `<span class="text-secondary text-xs font-weight-bold">${combined}</span>`;
                //}
                render: function (data) {
                    // Collect the fields in order
                    const fields = [data.CustomerArea, data.CustomerFacility, data.CustomerLocation];
                    // Filter out empty/null/undefined values and join with comma
                    const combined = fields.filter(f => f && f.trim() !== '').join(', ');

                    return `<span class="text-secondary text-xs font-weight-bold">${combined}</span>`;
                }
            },
            {
                data: 'InspectionStatus',
                render: function (status) {
                    const statusMap = {
                        1: { text: "Inspection Due", css: "bg-gradient-due" },
                        2: { text: "In Progress", css: "bg-gradient-inprogress" },
                        3: { text: "Send for Approval", css: "bg-gradient-sendforapproval" },
                        4: { text: "Completed", css: "bg-gradient-completed" },
                        5: { text: "Quotation Requested", css: "bg-gradient-requestquotation" },
                        6: { text: "Awaiting Approval", css: "bg-gradient-quotationawaitingapproval" },
                        7: { text: "Quotation Approved", css: "bg-gradient-quotationapproval" },
                        8: { text: "Repair Completed", css: "bg-gradient-repaircompleted" },
                        9: { text: "Finished", css: "bg-gradient-finished" }
                    };

                    const item = statusMap[status];
                    return item ? `<h6 class="badge badge-sm ${item.css}">${item.text}</h6>` : '';
                }
            },
            {
                data: 'InspectionId',
                orderable: false,
                render: function (id) {
                    return `<a href="/Admin/ManageInspectionFiles?id=${id}"><i class="material-icons text-dark">file_open</i></a>`;
                }
            },
            {
                data: 'InspectionId',
                orderable: false,
                render: function (id) {
                    return `<a href="/Admin/ToPdfV2?id=${id}"><i class="material-icons text-dark">download</i></a>`;
                }
            },
            //{
            //    data: 'InspectionId',
            //    orderable: false,
            //    render: function (id) {
            //        return `<a href="/Admin/DeleteInspectionDue?id=${id}"><i class="material-icons text-danger">delete</i></a>`;
            //    }
            //}
            {
                data: 'InspectionId',
                orderable: false,
                render: function (id) {
                    if (userType === "1") {
                        return `<a href="/Admin/DeleteInspectionDue?id=${id}" 
                        onclick="event.stopPropagation();">
                        <i class="material-icons text-danger">delete</i>
                    </a>`;
                    }
                    else
                    {
                        return '';
                    }
                    
                }
            }
        ]
    });

    $('#inspectionTable tbody').on('click', 'tr', function () {
        const data = table.row(this).data();
        if (data && data.InspectionId) {
            window.location.href = '/Admin/InspectionSheet?id=' + data.InspectionId;
        }
    });
});