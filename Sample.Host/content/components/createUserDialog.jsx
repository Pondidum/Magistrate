var Input = ReactBootstrap.Input;

var CreateUserDialog = React.createClass({

  showDialog(e) {
    e.preventDefault();
    this.refs.dialog.open();
  },

  onSubmit() {
    this.refs.dialog.close();
    console.log("create.submit");
  },

  getInitialState() {
    return {
      key: '',
      name: '',
      keyTaken: false,
    };
  },

  validateKey() {
    var key = this.state.key;

    if (key == null || key == '')
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

  render() {

    var keyHelp = this.state.keyTaken
      ? "This key is already in use"
      : "Unique identifier for the User";

    return (
      <a href="#" className="btn btn-primary" onClick={this.showDialog}>
        Create User
        <Dialog title="Create User" onSubmit={this.onSubmit} ref="dialog">
          <Input
            type="text"
            value={this.state.key}
            placeholder="e.g. 'BD659BC8-D5CE-43BC-A581-D41C534A3BE6'"
            label="Key"
            help={keyHelp}
            bsStyle={this.validateKey()}
            hasFeedback
            standalone
            ref="key"
            onChange={this.onKeyChanged} />
          <Input
            type="text"
            value={this.state.name}
            placeholder="Andy Dote"
            label="Name"
            help="Name of the user"
            bsStyle={this.validateName()}
            hasFeedback
            standalone
            ref="name"
            onChange={this.onNameChanged} />
        </Dialog>
      </a>
    );
  }

});
