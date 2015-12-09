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

  cancelEdit(e) {

    if (e)
      e.preventDefault();

    this.setState({
      editing: false,
      value: this.props.initialValue
    });

  },

  acceptEdit() {
    var newValue = this.refs.editor.value();
    console.log("Accept", newValue);

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
      <div className="input-group col-xs-12 col-sm-8">
        <input
          className="form-control"
          type="text"
          defaultValue={this.state.value}
          onKeyDown={this.onKeyDown}
          onFocus={ (e) => { e.target.select(); } }
          onBlur={this.cancelEdit}
          ref="editor"
          autoFocus
        />
        <div className="input-group-addon">
          <a href="#" onClick={this.cancelEdit}><span className="glyphicon glyphicon-remove" /></a>
        </div>
      </div>
    )
  }

});
