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

    $.ajax({
      url: this.props.url,
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
      }
    });

  },

  render() {
    return (
      <PermissionDialog onSubmit={this.onSubmit} ref="dialog" acceptText="Update" disableKey />
    );
  }
});
