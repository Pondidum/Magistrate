var InlineEditor = React.createClass({

  getInitialState() {
    return {
      editing: false,
      value: this.props.initialValue
    };
  },

  startEdit(e) {
    if (e) e.preventDefault();

    this.setState({
      editing: true
    });
  },

  cancelEdit(e) {
    if (e) e.preventDefault();

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
      return (
        <div className="inline-editor">
          <span>{this.state.value}</span>
          <a href="#" onClick={this.startEdit}>
            <span className="editor-glyph glyphicon glyphicon-pencil" />
          </a>
        </div>
      );

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
          <a href="#" onClick={this.cancelEdit}><span className="editor-glyph glyphicon glyphicon-remove" /></a>
        </div>
      </div>
    )
  }

});
