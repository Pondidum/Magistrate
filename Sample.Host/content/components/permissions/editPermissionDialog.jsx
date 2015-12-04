var EditPermissionDialog = React.createClass({

  open(permission, callback) {

    this.callback = callback;

    var dialog = this.refs.dialog;
    dialog.setValue(permission);
    dialog.open();
  },

  onSubmit() {

    var dialog = this.refs.dialog;
    var values = dialog.getValue();

    var json = JSON.stringify({
      name: values.name,
      description: values.description
    });

    dialog.asyncStart();

    var url = "/api/permissions/" + values.key;

    $.ajax({
      url: url,
      method: "PUT",
      data: json,
      cache: false,
      success: function() {
        dialog.asyncStop();
        this.callback(values);
        dialog.close();
      }.bind(this),
      error: function(xhr, status, err) {
        dialog.asyncStop();
        console.error(url, status, err.toString());
      }.bind(this)
    });

  },

  render() {
    return (
      <PermissionDialog onSubmit={this.onSubmit} ref="dialog" disableKey />
    );
  }
});
