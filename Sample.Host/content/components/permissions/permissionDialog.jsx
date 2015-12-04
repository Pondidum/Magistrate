var Input = ReactBootstrap.Input;

var PermissionDialog = React.createClass({

  getInitialState() {
    return {
      key: '',
      name: '',
      description: '',
      keyTaken: false,
    };
  },

  getValue() {
    return this.state;
  },

  setValue(value) {
    this.setState(value);
  },

  open() {
    this.refs.dialog.open();
  },

  close() {
    this.refs.dialog.close();
  },

  asyncStart() {
    this.refs.dialog.asyncStart();
  },

  asyncStop() {
    this.refs.dialog.asyncStop();
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
      <Dialog title="Create Permission" onSubmit={this.props.onSubmit} acceptText="Create" ref="dialog">
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
            onChange={this.onKeyChanged}
            disabled={this.props.disableKey}/>
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
    );
  }
});
