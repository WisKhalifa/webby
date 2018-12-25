$(document).ready(function () {

    displayToastr();

});

function displayToastr() {
    //alert('yes');
    // Display a info toast, with no title
    toastr.info('Info')

    // Display a warning toast, with no title
    toastr.warning('Warning!')

    // Display a success toast, with a title
    toastr.success('Comment Added!', 'Great job!')

    // Display an error toast, with a title
    toastr.error('Error!', 'Please contact with system administrator.')
}