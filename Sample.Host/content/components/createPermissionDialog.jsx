var Input = ReactBootstrap.Input;

var CreatePermissionDialog = React.createClass({

  showDialog(e) {
    e.preventDefault();
    this.refs.dialog.open();
  },

  onSubmit() {

    var dialog = this.refs.dialog;

    dialog.asyncStart();

    var json = JSON.stringify({
      key: this.state.key,
      name: this.state.name,
      description: this.state.description
    });

    $.ajax({
      url: "/api/permissions",
      method: "PUT",
      dataType: 'json',
      data: json,
      cache: false,
      success: function(data) {
        dialog.asyncStop();

        if (data.success) {
          this.props.onPermissionCreated(data.permission);
          dialog.close();
        } else {
          this.setState({ keyTaken: true });
        }

      }.bind(this),
      error: function(xhr, status, err) {
        dialog.asyncStop();
        console.error("/api/permissions", status, err.toString());
      }.bind(this)
    });

  },

  getInitialState() {
    return {
      key: '',
      name: '',
      description: '',
      keyTaken: false,
    };
  },

  validateKey() {
    var key = this.state.key;

    if (key == null || key == '' || this.state.keyTaken)
      return 'error';

    return 'success';
  },

  onKeyChanged() {
    this.setState({
      key: this.refs.key.getValue()
    });
  },

  validateName() {
    var name = this.state.name;

    if (name == null || name == '')
      return 'error';

    return 'success';
  },

  onNameChanged() {
    this.setState({
      name: this.refs.name.getValue()
    });
  },

  onDescriptionChanged() {
    this.setState({
      description: this.refs.description.getValue()
    });
  },

  render() {

    var keyHelp = this.state.keyTaken
      ? "This key is already in use"
      : "Unique identifier for the Permission";

    return (
      <a href="#" className="btn btn-primary" onClick={this.showDialog}>
        Create Permission
        <Dialog title="Create Permission" onSubmit={this.onSubmit} acceptText="Create" ref="dialog">
          <form>
            <Input
              type="text"
              value={this.state.key}
              placeholder="e.g. 'some-permission'"
              label="Key"
              help={keyHelp}
              bsStyle={this.validateKey()}
              hasFeedback
              autoFocus
              ref="key"
              onChange={this.onKeyChanged} />
            <Input
              type="text"
              value={this.state.name}
              placeholder="Some Permission"
              label="Name"
              help="Name of the permission"
              bsStyle={this.validateName()}
              hasFeedback
              ref="name"
              onChange={this.onNameChanged} />
            <Input
              type="textarea"
              value={this.state.description}
              placeholder="Allows users to ..."
              label="Description"
              help="A description of the permission"
              ref="description"
              onChange={this.onDescriptionChanged} />
          </form>
        </Dialog>
      </a>
    );
  }

});
