var EditPermissionDialog = React.createClass({

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
      dataType: 'json',
      data: json,
      cache: false,
      success: function(data) {
        dialog.asyncStop();
        this.props.onEdit(data);
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
