var InlineEditor = React.createClass({

  getInitialState() {
    return {
      editing: false,
      value: this.props.initialValue
    };
  },

  startEdit() {
    this.setState({
      editing: true
    });
  },

  cancelEdit() {
    this.setState({
      editing: false,
      value: this.props.initialValue
    });
  },

  acceptEdit() {
    var newValue = this.refs.editor.value;

    this.setState({
      editing: false,
      value: newValue
    });

    this.props.onChange(newValue);
  },

  onKeyDown(e) {
    if (e.key == "Escape")
      this.cancelEdit();

    if (e.key == "Enter")
      this.acceptEdit();
  },

  render() {

    if (this.state.editing == false)
      return (<span onClick={this.startEdit}>{this.state.value}</span>);

    return (
      <input
        type="text"
        defaultValue={this.state.value}
        onKeyDown={this.onKeyDown}
        onBlur={this.cancelEdit}
        onFocus={ (e) => { e.target.select(); } }
        ref="editor"
        autoFocus />
    )
  }

});
