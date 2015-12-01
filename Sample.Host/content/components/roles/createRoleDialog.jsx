var Input = ReactBootstrap.Input;

var CreateRoleDialog = React.createClass({

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
      url: "/api/roles",
      method: "PUT",
      dataType: 'json',
      data: json,
      cache: false,
      success: function(data) {
        dialog.asyncStop();

        if (data) {
          this.props.onCreate(data);
          dialog.close();
        } else {
          this.setState({ keyTaken: true });
        }

      }.bind(this),
      error: function(xhr, status, err) {
        dialog.asyncStop();
        console.error("/api/roles", status, err.toString());
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
      : "Unique identifier for the Role";

    return (
      <a href="#" className="btn btn-primary" onClick={this.showDialog}>
        Create Role
        <Dialog title="Create Role" onSubmit={this.onSubmit} acceptText="Create" ref="dialog">
          <form>
            <Input
              type="text"
              value={this.state.key}
              placeholder="e.g. 'some-role'"
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
              placeholder="Reviewer"
              label="Name"
              help="Name of the role"
              bsStyle={this.validateName()}
              hasFeedback
              ref="name"
              onChange={this.onNameChanged} />
            <Input
              type="textarea"
              value={this.state.description}
              placeholder="Includes all the permissions to do ..."
              label="Description"
              help="A description of the role"
              ref="description"
              onChange={this.onDescriptionChanged} />
          </form>
        </Dialog>
      </a>
    );
  }

});
